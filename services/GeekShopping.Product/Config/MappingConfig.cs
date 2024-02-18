using AutoMapper;
using GeekShopping.Product.Data.Dtos;

namespace GeekShopping.Product.Config;

public class MappingConfig 
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductDto, Model.Product>().ReverseMap();
        });

        return mappingConfig;
    }
}
