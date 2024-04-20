namespace GeekShopping.OrderApi.Messages;

public class CartDetailDto
{
    public int Id { get; set; }
    public long CartHeaderId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
    public virtual ProductDto? Product { get; set; }
}
