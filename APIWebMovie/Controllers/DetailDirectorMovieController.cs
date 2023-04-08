using APIWebMovie.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;

namespace APIMovieWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailDirectorMovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetailDirectorMovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("AddDetailDirectorMovie")]
        public async Task<IActionResult> AddDetailDirectorMovie(DetailDirectorView detail)
        {
            var result = await _unitOfWork.detailDirectorMovieRepository.Add<DetailDirectorView>(detail);
            if (result)
            {
                return Ok("Add success");
            }
            return BadRequest("Add failed");
        }

        [HttpPut("EditDetailDirectorMovie")]

        public async Task<IActionResult> EditDetailDirectorMovie(DetailDirectorView detailDirectorView)
        {
            var detail = await _unitOfWork.detailDirectorMovieRepository.FindToEntity(x => x.Id == detailDirectorView.Id);
            if (detail == null)
            {
                return NotFound();
            }
            detail.MovieId = detailDirectorView.MovieId;
            detail.DirectorId = detailDirectorView.DirectorId;
            var result = await _unitOfWork.detailDirectorMovieRepository.Update(detail);
            if (result)
            {
                return Ok("Edit success");
            }
            return BadRequest("Edit failed");
        }

        [HttpDelete("DeleteDetailDirectorMovie")]
        public async Task<IActionResult> DeleteDetailDirectorMovie(int Id)
        {
            var detail = await _unitOfWork.detailDirectorMovieRepository.FindToEntity(x => x.Id == Id);
            if (detail == null)
            {
                return NotFound();
            }
            var result = await _unitOfWork.detailDirectorMovieRepository.Delete(detail);
            if (result)
            {
                return Ok("Delete success");
            }
            return BadRequest("Delete failed");
        }
    }
}
