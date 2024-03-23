using AutoMapper;
using GeekShopping.CouponApi.Dtos;
using GeekShopping.CouponApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponApi.Repository;

public class CouponRepository : ICouponRepository
{
    private readonly MySQLContext _context;
    private readonly IMapper _mapper;

    public CouponRepository(MySQLContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CouponDto> GetCouponByCouponCodeAsync(string couponCode, CancellationToken cancellationToken)
        => _mapper.Map<CouponDto>(await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode, cancellationToken));
}
