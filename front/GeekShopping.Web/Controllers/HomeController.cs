using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GeekShopping.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using GeekShopping.Web.Services.Interfaces;

namespace GeekShopping.Web.Controllers;

public class HomeController(IProductService productService) : Controller
{
    public async Task<IActionResult> IndexAsync()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var products = await productService.FindAll(accessToken!);

        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
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
        return RedirectToAction(nameof(IndexAsync));
    }

    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }
}
