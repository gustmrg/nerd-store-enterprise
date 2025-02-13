using System.Text.RegularExpressions;

namespace NSE.Core.DomainObjects;

public class Email
{
    protected Email()
    {
        
    }

    public Email(string emailAddress)
    {
        if (!Validate(emailAddress)) throw new DomainException("Invalid email address");
            
        EmailAddress = emailAddress;
    }
    
    public const int MinLength = 5;
    public const int MaxLength = 255;
    public string EmailAddress { get; set; }

    public static bool Validate(string emailAddress)
    {
        var regexEmail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        return regexEmail.IsMatch(emailAddress);
    }
}