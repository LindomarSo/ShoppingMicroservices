using GeekShopping.OrderApi.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.OrderApi.Models;

[Table("order_detail")]
public class OrderDetail : BaseEntity
{
    [ForeignKey("OrderHeaderId")]
    public long OrderHeaderId { get; set; }

    [Column("ProductId")]
    public long ProductId { get; set; }

    [Column("count")]
    public int Count { get; set; }
    public virtual OrderHeader? OrderHeader { get; set; }

    [Column("product_name")]
    public string? ProductName { get; set; }

    [Column("price")]
    public decimal Price { get; set; }
}
