using NSE.WebApp.MVC.DTOs;

namespace NSE.WebApp.MVC.Models;

public class UserLoginResponse
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserToken UserToken { get; set; }
}