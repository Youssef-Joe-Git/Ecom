using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Config
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            //builder.HasData(
            //    new Photo { Id = 1, Name = "product1.jpg", ProductId = 1 },
            //    new Photo { Id = 2, Name = "product2.jpg", ProductId = 2 },
            //    new Photo { Id = 3, Name = "product3.jpg", ProductId = 3 }
            //);
        }
    }
}
