using GeekShopping.CartApi.Model.Base;

namespace GeekShopping.CartApi.Dtos;

public class CartHeaderDto : BaseEntity
{
    public string UserId { get; set; } = null!;
    public string CouponCode { get; set; } = null!;
}
