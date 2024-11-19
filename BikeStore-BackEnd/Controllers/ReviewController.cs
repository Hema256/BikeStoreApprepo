using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BikeStore_BackEnd.Dto; // Ensure this is the correct namespace for your ReviewDTO
using BikeStore_BackEnd.Iservices; // Ensure this is the correct namespace for IReviewService

namespace BikeStore_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: api/review
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        // GET: api/review/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTO>> GetReviewById(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        // GET: api/review/product/{productId}
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviewsByProductId(int productId)
        {
            var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);
            return Ok(reviews);
        }

        // POST: api/review
        [HttpPost]
        public async Task<ActionResult<ReviewDTO>> AddReview(ReviewDTO reviewDto)
        {
            var createdReview = await _reviewService.AddReviewAsync(reviewDto);
            return CreatedAtAction(nameof(GetReviewById), new { id = createdReview.ReviewId }, createdReview);
        }

        // PUT: api/review
        [HttpPut]
        public async Task<ActionResult<ReviewDTO>> UpdateReview(ReviewDTO reviewDto)
        {
            var updatedReview = await _reviewService.UpdateReviewAsync(reviewDto);
            if (updatedReview == null)
            {
                return NotFound();
            }
            return Ok(updatedReview);
        }

        // DELETE: api/review/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            await _reviewService.DeleteReviewAsync(id);
            return NoContent(); // 204 No Content
        }
    }
}
