using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public class CatalogService : BaseService, ICatalogService
{
    private readonly HttpClient _httpClient;
    private readonly AppSettings _appSettings;

    public CatalogService(HttpClient httpClient, IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_appSettings.CatalogApiUrl); 
        
    }
    
    public async Task<IEnumerable<ProductViewModel>> GetAll()
    {
        var response = await _httpClient.GetAsync("/api/catalog/products");
        
        HandleErrorResponse(response);
        
        return await DeserializeResponse<IEnumerable<ProductViewModel>>(response);
    }

    public async Task<ProductViewModel> GetById(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/catalog/products/{id}");
        
        HandleErrorResponse(response);
        
        return await DeserializeResponse<ProductViewModel>(response);
    }
}