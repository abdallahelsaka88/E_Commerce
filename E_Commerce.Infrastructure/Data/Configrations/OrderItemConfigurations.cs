using E_Commerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data.Configrations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(IO => IO.Price).HasColumnType("decimal(8,2)");



            builder.OwnsOne(OI => OI.Product, product =>
            {
                product.Property(P => P.ProductName).HasColumnName("ProductName")
                       .HasMaxLength(50);

                product.Property(P => P.PictureUrl).HasColumnName("PictureUrl")
                       .HasMaxLength(200);
            });


        }
    }
}
