using GeekShopping.CouponApi.Dtos;
using GeekShopping.CouponApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CouponApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CouponController : ControllerBase
{
    private readonly ICouponRepository _couponRepository;

    public CouponController(ICouponRepository couponRepository)
    {
        _couponRepository = couponRepository;
    }

    [HttpGet("get-coupon-by-code/{couponCode}")]
    public async Task<ActionResult<CouponDto>> GetCouponAsync(string couponCode, CancellationToken cancellation)
    {
        return Ok(await _couponRepository.GetCouponByCouponCodeAsync(couponCode, cancellation));
    }
}
