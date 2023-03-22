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
     //   [Route("GetAll")]
        public async Task<IActionResult> Get()
        {
            var movies = _unitOfWork.movieRepository.GetAll();
            return Ok(await movies);
        }
        [HttpGet("id")]
      //  [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var movies = _unitOfWork.movieRepository.GetById(id);
            return Ok(await movies);
        }
    }
}
