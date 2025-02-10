using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public class AuthenticationService : BaseService, IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly AppSettings _appSettings;

    public AuthenticationService(IOptions<AppSettings> appSettings)
    {
        _httpClient = new HttpClient();
        _appSettings = appSettings.Value;
    }

    public async Task<UserLoginResponse> LoginAsync(UserLoginInputModel userLoginInputModel)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_appSettings.IdentityApiUrl}/api/identity/login", userLoginInputModel);

        if (!HandleErrorResponse(response))
        {
            return new UserLoginResponse
            {
                ResponseResult = await DeserializeResponse<ResponseResult>(response)
            };
        }
        
        return await DeserializeResponse<UserLoginResponse>(response);
    }

    public async Task<UserLoginResponse> RegisterAsync(UserRegisterInputModel userRegisterInputModel)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_appSettings.IdentityApiUrl}/api/identity/register", userRegisterInputModel);
        
        if (!HandleErrorResponse(response))
        {
            return new UserLoginResponse
            {
                ResponseResult = await DeserializeResponse<ResponseResult>(response)
            };
        }
        
        return await DeserializeResponse<UserLoginResponse>(response);
    }
}