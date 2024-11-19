using AutoMapper;
using BikeStore_BackEnd.Data;
using BikeStore_BackEnd.Dto;
using BikeStore_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStore_BackEnd.Iservices.ServicesImpl
{
    public class ProductService : IProductService
    {
        private readonly BikeApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(BikeApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> AddProductAsync(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> UpdateProductAsync(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

    }
}
