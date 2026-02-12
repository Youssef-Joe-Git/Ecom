namespace Ecom.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IImageRepository Images { get; }
    }
}
