using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> FindAll(string token);
    Task<ProductModel?> FindById(long productId, string token);
    Task<ProductModel?> Create(ProductModel product, string token);
    Task<ProductModel?> Update(ProductModel product, string token);
    Task<bool> Delete(long productId, string token);
}
