using GeekShopping.Web.Extensions;
using GeekShopping.Web.Services.Interfaces;
using GeekShopping.Web.ViewModels;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private const string BasePath = "api/v1/product";

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductViewModel?> Create(ProductViewModel product, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.PostAsJsonAsync($"{BasePath}/create", product);

        return await response.ReadContentAs<ProductViewModel>();
    }

    public async Task<ProductViewModel?> Update(ProductViewModel product, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.PutAsJsonAsync($"{BasePath}/update", product);

        return await response.ReadContentAs<ProductViewModel>();
    }

    public async Task<IEnumerable<ProductViewModel>> FindAll(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BasePath}/find-all");

        var content = await response.ReadContentAs<IEnumerable<ProductViewModel>>();

        if (content == null)
            return Enumerable.Empty<ProductViewModel>();

        return content;
    }

    public async Task<ProductViewModel?> FindById(long productId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync($"{BasePath}/find-by-id/{productId}");

        return await response.ReadContentAs<ProductViewModel>();
    }

    public async Task<bool> Delete(long productId, string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.DeleteAsync($"{BasePath}/delete/{productId}");

        return await response.ReadContentAs<bool>();
    }
}
