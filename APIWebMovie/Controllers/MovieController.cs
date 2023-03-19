using APIWebMovie.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIWebMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public MovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var movies = _unitOfWork.movieRepository.GetAll();
            return Ok(await movies);
        }
    }
}
