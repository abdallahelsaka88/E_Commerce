using E_Commerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data.Configrations
{
    public class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name)
                   .HasMaxLength(100);


            builder.Property(P => P.Description)
                   .HasMaxLength(500);

            builder.Property(P => P.PictureUrl)
                   .HasMaxLength(200);


            builder.Property(P => P.Price)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(P => P.Brand)
                   .WithMany()
                   .HasForeignKey(P => P.BrandId);



            builder.HasOne(P => P.Type)
                   .WithMany()
                   .HasForeignKey(P => P.TypeId);


        }
    }
}
