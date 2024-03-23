namespace GeekShopping.Web.ViewModels;

public class CartViewModel
{
    public CartHeaderViewModel CartHeader { get; set; } = null!;
    public IEnumerable<CartDetailViewModel> CartDetail { get; set; } = Enumerable.Empty<CartDetailViewModel>();
}
