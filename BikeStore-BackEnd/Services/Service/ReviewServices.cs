using AutoMapper;
using BikeStore_BackEnd.Data;
using BikeStore_BackEnd.Dto;
using BikeStore_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeStore_BackEnd.Iservices.ServicesImpl
{
    public class ReviewService : IReviewService
    {
        private readonly BikeApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReviewService(BikeApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync()
        {
            var reviews = await _context.Reviews.ToListAsync();
            return _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
        }

        public async Task<ReviewDTO> GetReviewByIdAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewsByProductIdAsync(int productId)
        {
            var reviews = await _context.Reviews.Where(r => r. Product.ProductId== productId).ToListAsync();
            return _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
        }

        public async Task<ReviewDTO> AddReviewAsync(ReviewDTO reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<ReviewDTO> UpdateReviewAsync(ReviewDTO reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
