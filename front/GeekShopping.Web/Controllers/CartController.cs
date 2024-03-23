using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public CartController(IProductService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
    }

    [Authorize]
    public async Task<IActionResult> CartIndex()
    {
        CartViewModel? cart = await FindUserCart();
        return View(cart);
    }

    [Authorize]
    public async Task<IActionResult> Remove(int id)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.First(x => x.Type == "sub").Value;

        var response = await _cartService.RemoveFromCartAsync(id, token!);

        if (response)
        {
            RedirectToAction(nameof(CartIndex));
        }

        return View();
    }

    [Authorize]
    private async Task<CartViewModel?> FindUserCart()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.First(x => x.Type == "sub").Value;

        var cart = await _cartService.FindCartByUserIdAsync(userId, token!);

        if (cart is not null)
        {
            foreach (var detail in cart.CartDetail)
            {
                cart.CartHeader.PurchaseAmount += ((detail.Product!).Price * detail.Count);
            }
        }

        return cart;
    }
}
