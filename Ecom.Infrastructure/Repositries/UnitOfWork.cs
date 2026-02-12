using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;

namespace Ecom.Infrastructure.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageMS;


        public IProductRepository Products { get; } 
        public ICategoryRepository Categories { get; }
        public IImageRepository Images { get; }

        public UnitOfWork(
            AppDbContext context,
            IMapper mapper,
            IImageManagementService imageMS)
        {
            _context = context;
            _mapper = mapper;
            _imageMS = imageMS;

            Products = new ProductRepository(_context, _mapper, _imageMS);
            Categories = new CategoryRepository(_context);
            Images = new ImageRepository(_context);
        }
    }

}
