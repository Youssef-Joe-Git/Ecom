using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data.Config
{
    public class CategoryConfiguration: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic gadgets and devices" },
                new Category { Id = 2, Name = "Books", Description = "Various kinds of books" },
                new Category { Id = 3, Name = "Clothing", Description = "Apparel and garments" }
            );
        }
    }
}
