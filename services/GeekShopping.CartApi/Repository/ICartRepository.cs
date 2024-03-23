using GeekShopping.CartApi.Dtos;

namespace GeekShopping.CartApi.Repository;

public interface ICartRepository
{
    Task<CartDto> FindCartByUserIdAsync(string userId);
    Task<CartDto> SaveOrUpdateCartAsync(CartDto cart);
    Task<bool> RemoveFromCartAsync(long cartDetailsId);
    Task<bool> ApplyCouponAsync(string userId, string couponCode, CancellationToken cancellationToken);
    Task<bool> RemoveCouponAsync(string userId, CancellationToken cancellationToken);
    Task<bool> ClearCartAsync(string userId);
}
