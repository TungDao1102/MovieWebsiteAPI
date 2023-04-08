using APIWebMovie.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;

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
        [HttpPost("AddDetailGenresMovie")]
        public async Task<IActionResult> AddDetailGenresMovie(DetailGenresView detail)
        {
            var result = await _unitOfWork.detailGenresMovieRepository.Add<DetailGenresView>(detail);
            if (result)
            {
                return Ok("Add success");
            }
            return BadRequest("Add failed");
        }

        [HttpPut("EditDetailGenresMovie")]
        public async Task<IActionResult> EditDetailGenresMovie(DetailGenresView detailGenresView)
        {
            var detail = await _unitOfWork.detailGenresMovieRepository.FindToEntity(x => x.Id == detailGenresView.Id);
            if (detail == null)
            {
                return NotFound();
            }
            detail.MovieId = detailGenresView.MovieId;
            detail.GenresId = detailGenresView.GenresId;
            var result = await _unitOfWork.detailGenresMovieRepository.Update(detail);
            if (result)
            {
                return Ok("Edit success");
            }
            return BadRequest("Edit failed");
        }

        [HttpDelete("DeleteDetailGenresMovie")]
        public async Task<IActionResult> DeleteDetailGenresMovie(int Id)
        {
            var detail = await _unitOfWork.detailGenresMovieRepository.FindToEntity(x => x.Id == Id);
            if (detail == null)
            {
                return NotFound();
            }
            var result = await _unitOfWork.detailGenresMovieRepository.Delete(detail);
            if (result)
            {
                return Ok("Delete success");
            }
            return BadRequest("Delete failed");
        }
    }
}
