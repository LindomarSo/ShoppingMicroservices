using GeekShopping.Web.Extensions;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services;

public class CartService(HttpClient httpClient) : ICartService
{
    private const string BasePath = "api/v1/cart";

    public async Task<CartViewModel?> AddCartAsync(CartViewModel cart, string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await httpClient.PostAsJsonAsync($"{BasePath}/save-cart", cart);

        return await response.ReadContentAs<CartViewModel>();
    }

    public async Task<bool> ApplyCouponAsync(CartViewModel cart, string couponCode, string token)
    {
        throw new NotImplementedException();
    }

    public async Task<CartViewModel> CheckoutAsync(CartHeaderViewModel cart, string token)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ClearCartAsync(string userId, string token)
    {
        throw new NotImplementedException();
    }

    public async Task<CartViewModel?> FindCartByUserIdAsync(string userId, string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await httpClient.GetAsync($"{BasePath}/find-cart/{userId}");

        return await response.ReadContentAs<CartViewModel>();
    }

    public async Task<bool> RemoveCouponAsync(string userId, string token)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveFromCartAsync(long cartId, string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await httpClient.DeleteAsync($"{BasePath}/remove-cart/{cartId}");

        return await response.ReadContentAs<bool>();
    }

    public async Task<CartViewModel?> UpdateCartAsync(CartViewModel cart, string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await httpClient.PutAsJsonAsync($"{BasePath}/update-cart", cart);

        return await response.ReadContentAs<CartViewModel>();
    }
}
