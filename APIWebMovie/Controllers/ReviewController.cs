using APIWebMovie.Interface;
using APIWebMovie.Models;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;

namespace APIWebMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetAllReview")]
        public async Task<IActionResult> GetAllReview()
        {
            return Ok(await _unitOfWork.reviewRepository.GetAll<ReviewView>());
        }

        [HttpPost("PostReview")]
        public async Task<IActionResult> PostReview(ReviewView review)
        {
            var result = await _unitOfWork.reviewRepository.Add<ReviewView>(review);
            if (result)
            {
                return Ok("Post comment success");
            }
            return BadRequest("Post comment failed");    
        }

        [HttpDelete("DeleteReview")]
        public async Task<IActionResult> DeleteReview(int ReviewId)
        {
            var Review = await _unitOfWork.reviewRepository.FindToEntity( x => x.ReviewId == ReviewId );
            var result = await _unitOfWork.reviewRepository.Delete(Review);
            if(result)
            {
                return Ok("Delete success");
            }
            return BadRequest("Delete failed");
        }
    }
}
