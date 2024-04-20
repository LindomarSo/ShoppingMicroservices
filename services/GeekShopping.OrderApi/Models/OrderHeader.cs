using GeekShopping.OrderApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.OrderApi.Models;

[Table("order_header")]
public class OrderHeader : BaseEntity
{
    [Column("user_id")]
    [Required]
    [StringLength(150)]
    public string UserId { get; set; } = null!;

    [Column("coupon_code")]
    [StringLength(150)]
    public string? CouponCode { get; set; }

    [Column("purchase_amount")]
    public decimal PurchaseAmount { get; set; }

    [Column("discount_tootal")]
    public decimal DiscountTotal { get; set; }

    [Column("first_name")]
    public string? FirstName { get; set; }

    [Column("last_name")]
    public string? LastName { get; set; }

    [Column("purchase_date")]
    public DateTime DateTime { get; set; }

    [Column("order_time")]
    public DateTime OrderTime { get; set; }

    [Column("phone_number")]
    public string? PhoneNumber { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("ccv")]
    public string? CVV { get; set; }

    [Column("card_number")]
    public string? CardNumber { get; set; }

    [Column("expire_month_year")]
    public string? ExpireMonthYear { get; set; }

    [Column("total_items")]
    public int CartTotalItems { get; set; }

    [Column("payment_status")]
    public bool PaymentStatus { get; set; }

    public List<OrderDetail> OrderDetails { get; set; } = new();

}
