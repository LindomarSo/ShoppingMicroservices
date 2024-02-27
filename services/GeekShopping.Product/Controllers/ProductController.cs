using GeekShopping.Product.Data.Dtos;
using GeekShopping.Product.Repository;
using GeekShopping.Product.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Product.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("find-all")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> FindAll(CancellationToken cancellation)
        => Ok(await _repository.FindAll(cancellation));

    [HttpGet("find-by-id/{id}")]
    [Authorize]
    public async Task<ActionResult<ProductDto>> FindById(long id, CancellationToken cancellation)
        => Ok(await _repository.FindById(id, cancellation));

    [HttpPost("create")]
    [Authorize]
    public async Task<ActionResult<ProductDto>> Create([FromBody] ProductDto product, CancellationToken cancellation)
        => Ok(await _repository.Create(product, cancellation));

    [HttpPut("update")]
    [Authorize]
    public async Task<ActionResult<ProductDto>> Update([FromBody] ProductDto product, CancellationToken cancellation)
        => Ok(await _repository.Update(product, cancellation));

    [HttpDelete("delete/{id}")]
    [Authorize(Roles = Role.Admin)]
    public async Task<ActionResult<ProductDto>> Update(long id, CancellationToken cancellation)
        => Ok(await _repository.Delete(id, cancellation));
}
