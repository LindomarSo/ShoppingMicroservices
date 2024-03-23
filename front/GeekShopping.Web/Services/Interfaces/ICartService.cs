using GeekShopping.Web.ViewModels;

namespace GeekShopping.Web.Services.Interfaces;

public interface ICartService
{
    Task<CartViewModel?> FindCartByUserIdAsync(string userId, string token);
    Task<CartViewModel?> UpdateCartAsync(CartViewModel cart, string token);
    Task<CartViewModel?> AddCartAsync(CartViewModel cart, string token);
    Task<bool> RemoveFromCartAsync(long cartId, string token);
    Task<bool> ApplyCouponAsync(CartViewModel cart, string token);
    Task<bool> RemoveCouponAsync(string userId, string token);
    Task<bool> ClearCartAsync(string userId, string token);
    Task<CartViewModel?> CheckoutAsync(CartHeaderViewModel cart, string token);
}
