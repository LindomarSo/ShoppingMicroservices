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

    [HttpPost("apply-coupon")]
    public async Task<ActionResult<bool>> ApplyCouponAsync(CartDto cart, CancellationToken cancellationToken)
    {
        if (cart is { CartHeader: null } || string.IsNullOrEmpty(cart?.CartHeader?.CouponCode))
            return BadRequest();

        var status = await cartRepository.ApplyCouponAsync(cart.CartHeader.UserId, cart.CartHeader.CouponCode, cancellationToken);

        if (status)
            return Ok(status);

        return NotFound();
    }

    [HttpDelete("remove-coupon/{userId}")]
    public async Task<ActionResult<bool>> RemoveCouponAsync(string userId, CancellationToken cancellationToken)
    {
        var status = await cartRepository.RemoveCouponAsync(userId, cancellationToken);

        if (status)
            return Ok(status);

        return NotFound();
    }

    [HttpPost("checkout")]
    public async Task<ActionResult<bool>> CheckoutAsync(CancellationToken cancellationToken)
    {
        var status = await cartRepository.RemoveCouponAsync(userId, cancellationToken);

        if (status)
            return Ok(status);

        return NotFound();
    }
}
