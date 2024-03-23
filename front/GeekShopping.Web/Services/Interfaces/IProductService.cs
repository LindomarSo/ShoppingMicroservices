using GeekShopping.Web.ViewModels;

namespace GeekShopping.Web.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> FindAll(string token);
    Task<ProductViewModel?> FindById(long productId, string token);
    Task<ProductViewModel?> Create(ProductViewModel product, string token);
    Task<ProductViewModel?> Update(ProductViewModel product, string token);
    Task<bool> Delete(long productId, string token);
}
