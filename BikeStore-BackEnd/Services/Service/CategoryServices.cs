namespace BikeStore_BackEnd.Iservices.ServicesImpl
{
    using AutoMapper; // Assuming you're using AutoMapper for mapping
    using Microsoft.EntityFrameworkCore; // For DbContext
    using System.Collections.Generic; // For IEnumerable
    using System.Threading.Tasks; // For async methods
    using System.Linq; // For LINQ methods
    using BikeStore_BackEnd.Data;
    using BikeStore_BackEnd.Dto;
    using BikeStore_BackEnd.Models;

    public class CategoryService : ICategoryService
    {
        private readonly BikeApplicationDbContext _context;
        private readonly IMapper _mapper; // AutoMapper instance for mapping

        public CategoryService(BikeApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> UpdateCategoryAsync(int categoryId, CategoryDTO categoryDto)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) return null;

            _mapper.Map(categoryDto, category);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
