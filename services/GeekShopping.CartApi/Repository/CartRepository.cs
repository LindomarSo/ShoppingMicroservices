using AutoMapper;
using GeekShopping.CartApi.Dtos;
using GeekShopping.CartApi.Model;
using GeekShopping.CartApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartApi.Repository;

public class CartRepository(MySQLContext context, IMapper mapper) : ICartRepository
{
    public async Task<bool> ApplyCouponAsync(string userId, string couponCode)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ClearCartAsync(string userId)
    {
        var cartHeader = await context.CartHeaders.FirstOrDefaultAsync(d => d.UserId == userId);
        if (cartHeader != null)
        {
            await context.CartDetails
                            .AsNoTracking()
                            .Where(d => d.CartHeaderId == cartHeader.Id)
                            .ExecuteDeleteAsync();
            await context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<CartDto> FindCartByUserIdAsync(string userId)
    {
        Cart cart = new Cart
        {
            CartHeader = await context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(h => h.UserId == userId),
        };

        if (cart.CartHeader is not null)
        {
            cart.CartDetail = await context.CartDetails
                                        .AsNoTracking()
                                        .Where(d => d.CartHeaderId == cart.CartHeader.Id)
                                        .Include(x => x.Product)
                                        .ToListAsync();
        }

        return mapper.Map<CartDto>(cart);
    }

    public async Task<bool> RemoveCouponAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveFromCartAsync(long cartDetailsId)
    {
        try
        {
            var cartDetail = await context.CartDetails.FirstAsync(d => d.Id == cartDetailsId);
            int total = await context.CartDetails.Where(c => c.CartHeaderId == cartDetail.CartHeaderId).CountAsync();
            context.CartDetails.Remove(cartDetail);

            if (total == 1)
            {
                await context.CartHeaders
                    .Where(x => x.Id == cartDetail.CartHeaderId)
                    .ExecuteDeleteAsync();
            }

            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<CartDto> SaveOrUpdateCartAsync(CartDto cart)
    {
        var cartModel = mapper.Map<Cart>(cart);

        var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == cartModel.CartDetail.FirstOrDefault(new CartDetail()).ProductId);

        if (product == null)
        {
            var productDetail = cartModel.CartDetail.First().Product;
            if (productDetail != null)
            {
                context.Products.Add(productDetail);
                context.SaveChanges();
            }
        }

        CartHeader? cartHeader = null;
        if (cartModel.CartHeader is not null)
            cartHeader = await context.CartHeaders
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.UserId == cartModel.CartHeader.UserId);

        if (cartHeader == null && cartModel.CartHeader is not null)
        {
            context.CartHeaders.Add(cartModel.CartHeader);
            context.SaveChanges();
            var cartDetail = cartModel.CartDetail.FirstOrDefault();
            if (cartDetail != null)
            {
                cartDetail.CartHeaderId = cartModel.CartHeader.Id;
                cartDetail.Product = null;
                context.CartDetails.Add(cartModel.CartDetail.First());
                context.SaveChanges();
            }
        }
        else
        {
            var cartDetail = await context.CartDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ProductId == cartModel.CartDetail.First().ProductId && c.CartHeaderId == cartHeader.Id);

            if (cartDetail == null)
            {
                var cartDetailModel = cartModel.CartDetail.FirstOrDefault();
                if (cartDetailModel != null && cartHeader is not null)
                {
                    cartDetailModel.CartHeaderId = cartHeader.Id;
                    cartDetailModel.Product = null;
                    context.CartDetails.Add(cartModel.CartDetail.First());
                    context.SaveChanges();
                }
            }
            else
            {
                var cartDetailModel = cartModel.CartDetail.FirstOrDefault();
                if (cartDetailModel is not null)
                {
                    cartDetailModel.Product = null;
                    cartDetailModel.CartHeader = null;
                    cartDetailModel.Count += cartDetail.Count;
                    cartDetailModel.Id = cartDetail.Id;
                    cartDetailModel.CartHeaderId = cartDetail.CartHeaderId;
                    context.CartDetails.Update(cartDetailModel);
                    context.SaveChanges();
                }
            }
        }

        return mapper.Map<CartDto>(cartModel);
    }
}
