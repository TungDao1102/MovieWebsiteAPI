using APIWebMovie.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIWebMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DirectorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet ("id")]
        
        public async Task<IActionResult> GetByID(int id)
        {
            var director = _unitOfWork.directorRepository.GetById(id);
            return Ok(await director);
        }
    }
}
