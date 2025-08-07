using ECommerce.Domain.Entities.OrderItems;
using ECommerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {


        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Quantity)
                   .IsRequired();

            builder.Property(oi => oi.Price)
           .IsRequired()
           .HasPrecision(18, 2);

            builder.HasOne(oi => oi.Order)
           .WithMany(o => o.OrderItems)
           .HasForeignKey(oi => oi.OrderId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Product)
           .WithMany()
           .HasForeignKey(oi => oi.ProductId)
           .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
