using AutoMapper;
using GeekShopping.CouponApi.Dtos;
using GeekShopping.CouponApi.Model;

namespace GeekShopping.CouponApi.Config;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<CouponDto, Coupon>().ReverseMap();
        });

        return mappingConfig;
    }
}
