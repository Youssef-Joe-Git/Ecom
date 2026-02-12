using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Core.Shared;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastructure.Repositries
{
    public class ProductRepository : GenericRpository<Product>, IProductRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageMS;



        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageMS) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageMS = imageMS;
        }
        public async Task<IEnumerable<ProductDto>> GetAllAsync(ProductPrams productPrams)
        {
            var categoryId = productPrams.categoryId;
            var sort = productPrams.sort;
            var pageSize = productPrams.pageSize;
            var pageNumber = productPrams.pageNumber;
            var search = productPrams.search;





            var products = context.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .AsNoTracking();

            if(categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                products = sort.ToLower() switch
                {
                    "asc" => products = products.OrderBy(p => p.NewPrice),
                    "desc" => products = products.OrderByDescending(p => p.NewPrice),
                    _ => products = products.OrderBy(p => p.Id)
                };
            }

            if (!string.IsNullOrEmpty(search))
            {
                var  searchwords = search.Split(' ');


                products = products.Where(p => searchwords.All(word => 
                    p.Name.ToLower().Contains(word.ToLower()) ||
                    p.Description.ToLower().Contains(word.ToLower())));

            }
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;

            pageSize = pageSize <= 0 ? 10 : pageSize;
            pageSize = pageSize > 500 ? 500 : pageSize;

            products = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var result = mapper.Map<List<ProductDto>>(products);
            return result;
        }

        public async Task<bool> AddAsync(AddProductDto addProductDto)
        {
            if (addProductDto == null)
            {
                return  false;
            }
            var product = mapper.Map<Product>(addProductDto);

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var imagePath = await imageMS.AddImagesAsync(addProductDto.Images,addProductDto.Name);

            var productImages = imagePath.Select(path => new Image
            {
                Name = path,
                ProductId = product.Id
            }).ToList();

            await context.Images.AddRangeAsync(productImages);
            await context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> UpdateAsync(UpdateProductDto productDto)
        {
            if (productDto == null)
                return false;

            // جلب المنتج مع الصور والفئة
            var product = await context.Products
                                       .Include(p => p.Category)
                                       .Include(p => p.Images)
                                       .FirstOrDefaultAsync(p => p.Id == productDto.Id);

            if (product == null)
                return false;
            mapper.Map(productDto, product);

            var oldPhotos = product.Images.ToList();
            foreach (var photo in oldPhotos)
            {
                await imageMS.DeleteImageAsync(photo.Name); 
            }
            context.Images.RemoveRange(oldPhotos);

            var newImagePaths = await imageMS.AddImagesAsync(productDto.Images, productDto.Name);
            var newImages = newImagePaths.Select(path => new Image
            {
                Name = path,
                ProductId = product.Id
            }).ToList();

            context.Images.AddRange(newImages);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id == null || id == 0)
                return false;
            var product = context.Products.Include(p => p.Images).Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            if (product == null)
                return false;

            var Photos = product.Images.ToList();
            foreach (var photo in Photos)
            {
                await imageMS.DeleteImageAsync(photo.Name);
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return true;
        }
    }

}
