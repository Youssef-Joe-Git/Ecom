using Ecom.Core.Dtos;
using Ecom.Core.Entities.Product;
using Ecom.Core.Shared;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository: IGenericRpository<Product>
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(ProductPrams productPrams);
        Task<bool> AddAsync(AddProductDto addProductDto);
        Task<bool> UpdateAsync(UpdateProductDto productDto);
        Task<bool> DeleteAsync(int id);

    }
}
