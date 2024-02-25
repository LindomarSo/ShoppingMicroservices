using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

public class ProductController(IProductService productService) : Controller
{
    [Authorize]
    public async Task<IActionResult> ProductIndex()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var products = await productService.FindAll(accessToken!);

        return View(products);
    }

    public IActionResult ProductCreate()
    {
        return View();
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var product = await productService.Create(model, accessToken!);

            if (product is not null)
                return RedirectToAction(nameof(ProductIndex));
        }

        return View(model);
    }

    public async Task<IActionResult> ProductUpdate(long id)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var product = await productService.FindById(id, accessToken!);

        if (product is not null)
            return View(product);

        return RedirectToAction(nameof(ProductUpdate));
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ProductUpdate(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var product = await productService.Update(model, accessToken!);

            if (product is not null)
                return RedirectToAction(nameof(ProductIndex));
        }

        return View(model);
    }


    [Authorize]
    public async Task<IActionResult> ProductDelete(long id)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var product = await productService.FindById(id, accessToken!);

        if (product is not null)
            return View(product);

        return RedirectToAction(nameof(ProductDelete));
    }


    [HttpPost]
    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> ProductDelete(ProductModel model)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var product = await productService.Delete(model.Id, accessToken!);

        if (product)
            return RedirectToAction(nameof(ProductIndex));

        return View(model);
    }
}
