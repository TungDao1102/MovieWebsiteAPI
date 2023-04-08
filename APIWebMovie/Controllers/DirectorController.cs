using APIWebMovie.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;
using System.Linq.Expressions;

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

        [HttpGet("GetAllDirector")]
        public async Task<IActionResult> GetAllDirector()
        {
            var directors = await _unitOfWork.directorRepository.FindToList<ActorView>(x => !x.IsDelete);
            if (directors == null)
            {
                return NotFound();
            }
            return Ok(directors);
        }

        [HttpGet("GetDirectorById")]
        public async Task<IActionResult> GetDirectorById(int directorId)
        {
            var director = await _unitOfWork.directorRepository.Find<DirectorView>(x => x.DirectorId == directorId && !x.IsDelete);
            if (director == null)
            {
                return NotFound("Director not found");
            }
            return Ok(director);
        }

        [HttpPost("AddDirector")]
        public async Task<IActionResult> AddDirector(DirectorView director)
        {
            var result = await _unitOfWork.directorRepository.Add<DirectorView>(director);
            if (result)
            {
                return Ok("Add success");
            }
            return BadRequest("Add failed");
        }

        [HttpPut("EditDirector")]
        public async Task<IActionResult> EditDirector(DirectorView directorView)
        {
            var director = await _unitOfWork.directorRepository.FindToEntity(x => x.DirectorId == directorView.DirectorId);
            if (director == null)
            {
                return NotFound();
            }
            director.DirectorName = directorView.DirectorName;
            director.IsDelete = directorView.IsDelete;
            var result = await _unitOfWork.directorRepository.Update(director);
            if (result)
            {
                return Ok("Edit success");
            }
            return BadRequest("Edit failed");
        }

        [HttpDelete("DeleteDirector")]
        public async Task<IActionResult> DeleteDirector(int directorId)
        {
            var director = await _unitOfWork.directorRepository.FindToEntity(x => x.DirectorId == directorId && !x.IsDelete);
            if (director == null)
            {
                return NotFound("Director not found");
            }
            director.IsDelete = true;
            var result = await _unitOfWork.directorRepository.Update(director);
            if (result)
            {
                return Ok("Delete success");
            }
            return BadRequest("Delete failed");
        }

        [HttpGet("GetDirectorByMovieId")]

        public async Task<IActionResult> GetDirectorByMovieId(int movieId)
        {
            var details = await _unitOfWork.detailDirectorMovieRepository.FindToList<DetailDirectorView>(x => x.MovieId == movieId);
            if (details == null)
            {
                return NotFound();
            }
            var listDirector = new List<DirectorView>();
            foreach (var detail in details)
            {
                var director = await _unitOfWork.directorRepository.Find<DirectorView>(x => x.DirectorId == detail.DirectorId && !x.IsDelete);
                listDirector.Add(director);
            }
            if (listDirector.Count > 0)
            {
                return Ok(listDirector);
            }
            return NotFound();
        }
    }
}
