using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> FindAll();
    Task<ProductModel?> FindById(long productId);
    Task<ProductModel?> Create(ProductModel product);
    Task<ProductModel?> Update(ProductModel product);
    Task<bool> Delete(long productId);
}
