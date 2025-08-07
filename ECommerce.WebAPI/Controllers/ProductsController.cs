using ECommerce.Application.Products.Commands;
using ECommerce.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // POST: api/products
        [HttpPost(Name = "CreateProduct")]

        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAll), new { id = productId }, productId);
        }

        // GET: api/products
        [HttpGet(Name = "GetAllProduct")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(product);
        }
        [HttpPut("{id}", Name = "UpdateProduct")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest("URL'deki ID ile body'deki ID eşleşmiyor.");

            await _mediator.Send(command);
            return NoContent(); // Başarılı güncelleme için içerik dönülmez
        }
        [HttpDelete("{id}", Name = "DeleteProduct")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
    }
}
