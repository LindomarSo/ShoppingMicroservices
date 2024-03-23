using GeekShopping.Web.Services.Interfaces;
using GeekShopping.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    private readonly ICouponService _couponService;

    public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
    {
        _productService = productService;
        _cartService = cartService;
        _couponService = couponService;
    }

    [Authorize]
    public async Task<IActionResult> CartIndex(CancellationToken cancellation)
    {
        CartViewModel? cart = await FindUserCart(cancellation);
        return View(cart);
    }

    [Authorize]
    [HttpPost]
    [ActionName("ApplyCoupon")]
    public async Task<IActionResult> ApplyCoupon(CartViewModel cart)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var status = await _cartService.ApplyCouponAsync(cart, token!);

        if (status)
        {
            return RedirectToAction(nameof(CartIndex));
        }

        return View();
    }

    [Authorize]
    [ActionName("RemoveCoupon")]
    public async Task<IActionResult> RemoveCoupon()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.First(x => x.Type == "sub").Value;
        var status = await _cartService.RemoveCouponAsync(userId, token!);

        if (status)
        {
            return RedirectToAction(nameof(CartIndex));
        }

        return View();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Checkout(CancellationToken cancellation)
    {
        CartViewModel? cart = await FindUserCart(cancellation);
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

    private async Task<CartViewModel?> FindUserCart(CancellationToken cancellation)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var userId = User.Claims.First(x => x.Type == "sub").Value;

        var cart = await _cartService.FindCartByUserIdAsync(userId, token!);

        if (cart is not null)
        {
            if(!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
            {
                var coupon = await _couponService.GetCouponByCouponCodeAsync(cart.CartHeader.CouponCode, token!, cancellation);

                if(coupon is { CouponCode.Length: > 0 })
                {
                    cart.CartHeader.DiscountTotal = coupon.DiscountAmount;
                }
            }

            foreach (var detail in cart.CartDetail)
            {
                cart.CartHeader.PurchaseAmount += ((detail.Product!).Price * detail.Count);
            }

            cart.CartHeader.PurchaseAmount -= cart.CartHeader.DiscountTotal;
        }

        return cart;
    }
}
