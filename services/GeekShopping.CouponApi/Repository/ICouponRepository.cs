using GeekShopping.CouponApi.Dtos;

namespace GeekShopping.CouponApi.Repository;

public interface ICouponRepository
{
    Task<CouponDto> GetCouponByCouponCodeAsync(string couponCode, CancellationToken cancellationToken);
}
