namespace GeekShopping.Web.Models;

public class CartViewModel
{
    public CartHeaderViewModel CartHeader { get; set; } = null!;
    public IEnumerable<CartDetailViewModel> CartDetail { get; set; } = Enumerable.Empty<CartDetailViewModel>();
}
