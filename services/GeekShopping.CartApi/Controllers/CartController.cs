using GeekShopping.CartApi.Dtos;
using GeekShopping.CartApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartApi.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class CartController(ICartRepository cartRepository) : ControllerBase
{
    [HttpGet("find-cart/{userId}")]
    public async Task<ActionResult<CartDto>> FindById(string userId)
    {
        return Ok(await cartRepository.FindCartByUserIdAsync(userId));
    }

    [HttpPost("save-cart")]
    public async Task<ActionResult<CartDto>> AddCart(CartDto cart)
    {
        return Ok(await cartRepository.SaveOrUpdateCartAsync(cart));
    }

    [HttpPut("update-cart")]
    public async Task<ActionResult<CartDto>> UpdateCart(CartDto cart)
    {
        return Ok(await cartRepository.SaveOrUpdateCartAsync(cart));
    }

    [HttpDelete("remove-cart/{id}")]
    public async Task<ActionResult<bool>> RemoveCart(long id)
    {
        return Ok(await cartRepository.RemoveFromCartAsync(id));
    }
}
