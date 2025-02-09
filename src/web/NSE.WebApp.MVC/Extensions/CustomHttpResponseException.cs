using System.Net;

namespace NSE.WebApp.MVC.Extensions;

public class CustomHttpResponseException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public CustomHttpResponseException()
    {
        
    }

    public CustomHttpResponseException(string message, Exception innerException) : base(message, innerException)
    {
        
    }

    public CustomHttpResponseException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }
}