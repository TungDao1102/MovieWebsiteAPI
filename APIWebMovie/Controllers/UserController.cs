using APIWebMovie.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIWebMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> getUser()
        {
            return Ok(await _unitOfWork.userRepository.GetAll());
        }
    }
}
