using GeekShopping.Product.Data.Dtos;

namespace GeekShopping.Product.Repository;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> FindAll(CancellationToken cancellation);
    Task<ProductDto> FindById(long productId, CancellationToken cancellation);
    Task<ProductDto> Create(ProductDto product, CancellationToken cancellation);
    Task<ProductDto> Update(ProductDto product, CancellationToken cancellation);
    Task<bool> Delete(long productId, CancellationToken cancellation);
}
