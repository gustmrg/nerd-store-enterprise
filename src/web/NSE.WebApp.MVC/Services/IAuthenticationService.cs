using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public interface IAuthenticationService
{
    public Task<UserLoginResponse> LoginAsync(UserLoginInputModel userLoginInputModel);
    public Task<UserLoginResponse> RegisterAsync(UserRegisterInputModel userRegisterInputModel);
}