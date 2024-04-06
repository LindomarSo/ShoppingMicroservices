using GeekShopping.CartApi.Dtos;
using GeekShopping.CartApi.Messages;
using GeekShopping.CartApi.RabbitMQSender;
using GeekShopping.CartApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartApi.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class CartController(ICartRepository cartRepository, IRabbitMQMessageSender rabbitMQMessageSender) : ControllerBase
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
    public async Task<ActionResult<CheckoutHeaderDto>> CheckoutAsync(CheckoutHeaderDto checkout, CancellationToken cancellationToken)
    {
        if(checkout?.UserId == null)
        {
            return BadRequest();
        }

        var cart = await cartRepository.FindCartByUserIdAsync(checkout.UserId);

        if(cart is null)
        {
            return NotFound();
        }

        checkout.CartDetails = cart.CartDetail;
        checkout.DateTime = DateTime.Now;

        // TODO: RabbitMQ logic comes here
        rabbitMQMessageSender.SendMessage(checkout, "checkoutQueue");

        return Ok(checkout);
    }
}
