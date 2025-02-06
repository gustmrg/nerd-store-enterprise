namespace NSE.Identity.API.Models;

public class UserLoginResponseModel
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserToken UserToken { get; set; }
}