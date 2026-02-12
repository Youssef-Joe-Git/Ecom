using Ecom.Core.Dtos;
using Ecom.Core.Entities.Product;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository: IGenericRpository<Product>
    {
        Task<bool> AddAsync(AddProductDto addProductDto);
        Task<bool> UpdateAsync(UpdateProductDto productDto);
        Task<bool> DeleteAsync(int id);

    }
}
