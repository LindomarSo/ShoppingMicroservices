namespace GeekShopping.CartApi.Dtos;

public class CartDetailDto
{
    public int Id { get; set; }
    public long CartHeaderId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
    public CartHeaderDto? CartHeader { get; set; }
    public ProductDto? Product { get; set; }
}
