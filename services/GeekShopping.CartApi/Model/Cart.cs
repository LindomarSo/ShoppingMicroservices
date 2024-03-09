namespace GeekShopping.CartApi.Model;

public class Cart
{
    public CartHeader CartHeader { get; set; } = null!;
    public IEnumerable<CartDetail> CartDetail { get; set; } = Enumerable.Empty<CartDetail>();
}
