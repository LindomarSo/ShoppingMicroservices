using GeekShopping.CartApi.Dtos;

namespace GeekShopping.CartApi.Messages;

public class CheckoutHeaderDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public string CouponCode { get; set; } = null!;
    public decimal PurchaseAmount { get; set; }
    public decimal DiscountTotal { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateTime { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? CVV { get; set; }
    public string? CardNumber { get; set; }
    public string? ExpireMonthYear { get; set; }
    public int CartTotalItems { get; set; }
    public IEnumerable<CartDetailDto> CartDetails { get; set; } = Enumerable.Empty<CartDetailDto>();
}
