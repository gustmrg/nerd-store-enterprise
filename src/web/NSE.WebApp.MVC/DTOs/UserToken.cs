namespace NSE.WebApp.MVC.DTOs;

public record UserToken(string Id, string Email, IEnumerable<UserClaim> Claims);