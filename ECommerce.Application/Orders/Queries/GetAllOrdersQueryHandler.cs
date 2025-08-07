using AutoMapper;
using ECommerce.Application.Orders.Dtos;
using ECommerce.Application.Orders.Rules;
using ECommerce.Application.Products.Dtos;
using ECommerce.Application.Products.Queries;
using ECommerce.Application.Products.Rules;
using ECommerce.Domain.Abstractions.Repositories;
using ECommerce.Domain.Entities.Orders;
using ECommerce.Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Orders.Queries
{
    public sealed class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IReadOnlyList<OrderDto>>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        private readonly IOrderBusinessRules _orderBusinessRules;

        public GetAllOrdersQueryHandler(IRepository<Order> orderRepository, IMapper mapper, IOrderBusinessRules orderBusinessRules)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderBusinessRules = orderBusinessRules;
        }

        public async Task<IReadOnlyList<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(
                include: query => query
                    .Include(o => o.User)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product),
                cancellationToken: cancellationToken);

            _orderBusinessRules.CheckAllOrders(orders);

            return _mapper.Map<IReadOnlyList<OrderDto>>(orders);
        }
    }
}
