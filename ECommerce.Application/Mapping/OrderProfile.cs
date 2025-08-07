using AutoMapper;
using ECommerce.Application.Orders.Dtos;
using ECommerce.Domain.Entities.OrderItems;
using ECommerce.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Mapping
{
    public class OrderProfile:Profile
    {

        public OrderProfile()
        {
            // Order → OrderDto
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName));

            // OrderItem → OrderItemDto
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

        }
    }
}
