using GeekShopping.CartApi.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartApi.Model;

[Table("cart_header")]
public class CartHeader : BaseEntity
{
    [Column("user_id")]
    [Required]
    [StringLength(150)]
    public string UserId { get; set; } = null!;

    [Column("coupon_code")]
    [Required]
    [StringLength(150)]
    public string CouponCode { get; set; } = null!;

}
