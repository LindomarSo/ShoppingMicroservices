using AutoMapper;
using GeekShopping.Product.Data.Dtos;
using GeekShopping.Product.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Product.Repository;

public class ProductRepository : IProductRepository
{
    private readonly MySQLContext _context;
    private readonly IMapper _mapper;

    public ProductRepository(MySQLContext context,  IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ProductDto> Create(ProductDto product, CancellationToken cancellation)
    {
        var model = _mapper.Map<Model.Product>(product);

        await _context.Products.AddAsync(model, cancellation);
        await _context.SaveChangesAsync(cancellation);

        return _mapper.Map<ProductDto>(model);
    }

    public async Task<ProductDto> Update(ProductDto product, CancellationToken cancellation)
    {
        var model = _mapper.Map<Model.Product>(product);

        _context.Products.Update(model);
        await _context.SaveChangesAsync(cancellation);

        return _mapper.Map<ProductDto>(model);
    }

    public async Task<IEnumerable<ProductDto>> FindAll(CancellationToken cancellation) 
        => _mapper.Map<IEnumerable<ProductDto>>(await _context.Products.AsNoTracking().ToListAsync(cancellation));

    public async Task<ProductDto> FindById(long productId, CancellationToken cancellation)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == productId, cancellation);

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<bool> Delete(long productId, CancellationToken cancellation)
    {
        try
        {
            var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == productId, cancellation);

            if (product is null) return false;

            _context.Remove(product);
            await _context.SaveChangesAsync(cancellation);

            return true;
        }
        catch  
        {
            return false;
        }
    }
}
