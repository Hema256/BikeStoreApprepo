using BikeStore_BackEnd.Dto;

namespace BikeStore_BackEnd.Iservices
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync();
        Task<ReviewDTO> GetReviewByIdAsync(int id);
        Task<IEnumerable<ReviewDTO>> GetReviewsByProductIdAsync(int productId);
        Task<ReviewDTO> AddReviewAsync(ReviewDTO reviewDto);
        Task<ReviewDTO> UpdateReviewAsync(ReviewDTO reviewDto);
        Task DeleteReviewAsync(int id);
    }
}
