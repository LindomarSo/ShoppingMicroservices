namespace GeekShopping.CartApi.Dtos;

public class CartHeaderDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string? CouponCode { get; set; }
}
