using GeekShopping.Web.ViewModels;

namespace GeekShopping.Web.Services.Interfaces;

public interface ICouponService
{
    Task<CouponViewModel?> GetCouponByCouponCodeAsync(string couponCode, string token, CancellationToken cancellationToken);
}
