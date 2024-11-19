namespace BikeStore_BackEnd.Iservices
{
    using BikeStore_BackEnd.Dto;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int categoryId);
        Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDto);
        Task<CategoryDTO> UpdateCategoryAsync(int categoryId, CategoryDTO categoryDto);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }

}
