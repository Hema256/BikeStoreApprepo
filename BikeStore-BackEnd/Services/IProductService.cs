using BikeStore_BackEnd.Dto;

namespace BikeStore_BackEnd.Iservices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<ProductDTO> AddProductAsync(ProductDTO productDto);
        Task<ProductDTO> UpdateProductAsync(ProductDTO productDto);
        Task DeleteProductAsync(int id);
    }
}
