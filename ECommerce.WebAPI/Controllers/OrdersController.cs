using ECommerce.Application.Orders.Commands;
using ECommerce.Application.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }



     
        [HttpPost(Name = "CreateOrder")]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = orderId }, orderId);
        }

   
        [HttpGet(Name = "GetAllOrders")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(orders);
        }

        
        [HttpGet("{id}", Name = "GetOrderById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(id));
            return Ok(order);
        }
    }
}
