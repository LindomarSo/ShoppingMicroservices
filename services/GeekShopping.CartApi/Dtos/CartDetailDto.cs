using GeekShopping.CartApi.Model.Base;

namespace GeekShopping.CartApi.Dtos;

public class CartDetailDto : BaseEntity
{
    public long CartHeaderId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
    public CartHeaderDto? CartHeader { get; set; }
    public ProductDto? Product { get; set; }
}
