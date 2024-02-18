using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

public class ProductController(IProductService productService) : Controller
{
    public async Task<IActionResult> ProductIndex()
    {
        var products = await productService.FindAll();

        return View(products);
    }

    public IActionResult ProductCreate()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var product = await productService.Create(model);

            if (product is not null)
                return RedirectToAction(nameof(ProductIndex));
        }

        return View(model);
    }

    public async Task<IActionResult> ProductUpdate(long id)
    {
        var product = await productService.FindById(id);

        if (product is not null)
            return View(product);

        return RedirectToAction(nameof(ProductUpdate));
    }

    [HttpPost]
    public async Task<IActionResult> ProductUpdate(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var product = await productService.Update(model);

            if (product is not null)
                return RedirectToAction(nameof(ProductIndex));
        }

        return View(model);
    }

    public async Task<IActionResult> ProductDelete(long id)
    {
        var product = await productService.FindById(id);

        if (product is not null)
            return View(product);

        return RedirectToAction(nameof(ProductDelete));
    }

    [HttpPost]
    public async Task<IActionResult> ProductDelete(ProductModel model)
    {
            var product = await productService.Delete(model.Id);

            if (product)
                return RedirectToAction(nameof(ProductIndex));

        return View(model);
    }
}
