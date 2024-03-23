using GeekShopping.Web.Services.Interfaces;
using GeekShopping.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GeekShopping.Web.Controllers;

public class HomeController(IProductService productService, ICartService cartService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var products = await productService.FindAll(string.Empty);

        return View(products);
    }

    [Authorize]
    public async Task<IActionResult> Details(int id)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var product = await productService.FindById(id, token!);
        return View(product);
    }

    [Authorize]
    [HttpPost]
    [ActionName("Details")]
    public async Task<IActionResult> DetailsPost(ProductViewModel product)
    {
        string token = (await HttpContext.GetTokenAsync("access_token"))!;

        CartViewModel cart = new()
        {
            CartHeader = new CartHeaderViewModel
            {
                UserId = User.Claims.First(x => x.Type == "sub").Value,
            }
        };

        CartDetailViewModel cartDetail = new()
        {
            Count = product.Count,
            ProductId = product.Id,
            Product = await productService.FindById(product.Id, token),
        };


        var cartDetailList = new List<CartDetailViewModel>
        {
            cartDetail
        };

        cart.CartDetail = cartDetailList;

        var response = await cartService.AddCartAsync(cart, token);

        if(response != null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize]
    public async Task<IActionResult> Login()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }
}
