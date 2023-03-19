using APIWebMovie.Interface;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.reviewRepository.GetAll());
        }
    }
}
