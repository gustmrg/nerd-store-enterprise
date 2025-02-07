using System.Text.Json;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService()
    {
        _httpClient = new HttpClient();
    }
    
    public async Task<UserLoginResponse> LoginAsync(UserLoginInputModel userLoginInputModel)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5143/api/identity/login", userLoginInputModel, options);
        var responseContent = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<UserLoginResponse>(responseContent, options);
    }

    public async Task<UserLoginResponse> RegisterAsync(UserRegisterInputModel userRegisterInputModel)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5143/api/identity/register", userRegisterInputModel, options);
        var responseContent = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<UserLoginResponse>(responseContent, options);
    }
}