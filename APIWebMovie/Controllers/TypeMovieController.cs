using APIWebMovie.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;

namespace APIWebMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeMovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TypeMovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetTypeMovie")]
        public async Task<IActionResult> GetTypeMovie()
        {
            var types = await _unitOfWork.typeMovieRepository.GetAll<TypeMovieView>();
            if(types == null)
            {
                return NotFound();
            }
            return Ok(types);
        }
    }
}
