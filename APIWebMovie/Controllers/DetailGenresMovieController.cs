using APIWebMovie.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIWebMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailGenresMovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public DetailGenresMovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            try {
                var listDetailGenres = await _unitOfWork.detailGenresMovieRepository.GetAll();
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
