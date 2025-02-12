using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Services;

public abstract class BaseService
{
    protected bool HandleErrorResponse(HttpResponseMessage response)
    {
        switch ((int)response.StatusCode)
        {
            case 400:
                return false;
            case 401:
            case 403:
            case 404:
            case 500:
                throw new CustomHttpResponseException(response.StatusCode);
        }
        
        response.EnsureSuccessStatusCode();
        return true;
    }

    protected static async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), options);
    }
}