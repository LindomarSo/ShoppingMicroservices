namespace GeekShopping.CartApi.Dtos;

public class CartDto
{
    public CartHeaderDto CartHeader { get; set; } = null!;
    public IEnumerable<CartDetailDto> CartDetail { get; set; } = Enumerable.Empty<CartDetailDto>();
}
