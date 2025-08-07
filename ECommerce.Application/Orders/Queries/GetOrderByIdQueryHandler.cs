using AutoMapper;
using ECommerce.Application.Orders.Dtos;
using ECommerce.Application.Orders.Rules;
using ECommerce.Domain.Abstractions.Repositories;
using ECommerce.Domain.Entities.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Orders.Queries
{
    public sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        private readonly IOrderBusinessRules _orderBusinessRules;

        public GetOrderByIdQueryHandler(IRepository<Order> orderRepository, IMapper mapper, IOrderBusinessRules orderBusinessRules)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderBusinessRules = orderBusinessRules;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(
                request.Id,
                include: query => query
                    .Include(o => o.User)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product),
                cancellationToken: cancellationToken);

            _orderBusinessRules.CheckOrderExists(order, request.Id);

            return _mapper.Map<OrderDto>(order);
        }
    }
}
