using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            //builder.HasData(
            //    new Product { Id = 1, Name = "Smartphone", Description = "Latest model smartphone", Price = 699.99m, CategoryId = 1 },
            //    new Product { Id = 2, Name = "Novel Book", Description = "Bestselling novel", Price = 19.99m, CategoryId = 2 },
            //    new Product { Id = 3, Name = "Jeans", Description = "Comfortable blue jeans", Price = 49.99m, CategoryId = 3 }
            //);
        }
    }
}
