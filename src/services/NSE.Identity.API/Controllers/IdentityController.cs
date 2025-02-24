using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSE.Core.Messages.Integration;
using NSE.Identity.API.Models;
using NSE.MessageBus;
using NSE.WebAPI.Core.Identity;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace NSE.Identity.API.Controllers;

[Route("api/identity")]
public class IdentityController : MainController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppSettings _appSettings;
    private readonly IMessageBus _messageBus;

    public IdentityController(SignInManager<IdentityUser> signInManager, 
        UserManager<IdentityUser> userManager, 
        IOptions<AppSettings> appSettings, 
        IMessageBus messageBus)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _messageBus = messageBus;
        _appSettings = appSettings.Value;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] UserRegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return CustomResponse(ModelState);
        }

        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true
        };
        
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var customerResult = await CreateCustomer(model);

            if (!customerResult.ValidationResult.IsValid)
            {
                await _userManager.DeleteAsync(user);
                return CustomResponse(customerResult.ValidationResult);
            }
            
            return CustomResponse(await GenerateJwtTokens(model.Email));
        }

        foreach (var error in result.Errors)
        {
            AddError(error.Description);
        }
        
        return CustomResponse();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return CustomResponse(ModelState);
        }
        
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

        if (result.Succeeded)
        {
            return CustomResponse(await GenerateJwtTokens(model.Email));
        }

        if (result.IsLockedOut)
        {
            AddError("User account temporarily locked out for too many wrong attempts.");
            return CustomResponse();
        }
        
        AddError("Invalid login or password.");
        return BadRequest();
    }

    private async Task<UserLoginResponseModel> GenerateJwtTokens(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);
        
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

        foreach (var role in userRoles)
        {
            claims.Add(new Claim("role", role));
        }
        
        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.Audience,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });
        
        var encodedToken = tokenHandler.WriteToken(token);

        var response = new UserLoginResponseModel
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
            UserToken = new UserToken
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
            }
        };

        return response;
    }
    
    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    
    private async Task<ResponseMessage> CreateCustomer(UserRegisterViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        var userRegister = new CreatedUserIntegrationEvent(Guid.Parse(user.Id), model.Name, model.Email, model.DocumentNumber);

        try
        {
            return await _messageBus.RequestAsync<CreatedUserIntegrationEvent, ResponseMessage>(userRegister);
        }
        catch
        {
            await _userManager.DeleteAsync(user);
            throw;
        }
    }
}