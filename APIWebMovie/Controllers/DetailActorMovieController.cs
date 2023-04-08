using APIWebMovie.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;

namespace APIMovieWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailActorMovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetailActorMovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("AddDetailActorMovie")]
        public async Task<IActionResult> AddDetailActorMovie(DetailActorView detail)
        {
            var result = await _unitOfWork.detailActorMovieRepository.Add<DetailActorView>(detail);
            if (result)
            {
                return Ok("Add success");
            }
            return BadRequest("Add failed");
        }

        [HttpPut("EditDetailActorMovie")]

        public async Task<IActionResult> EditDetailActorMovie(DetailActorView detailActorView)
        {
            var detail = await _unitOfWork.detailActorMovieRepository.FindToEntity(x => x.Id == detailActorView.Id);
            if (detail == null)
            {
                return NotFound();
            }
            detail.MovieId = detailActorView.MovieId;
            detail.ActorId = detailActorView.ActorId;
            var result = await _unitOfWork.detailActorMovieRepository.Update(detail);
            if (result)
            {
                return Ok("Edit success");
            }
            return BadRequest("Edit failed");
        }

        [HttpDelete("DeleteDetailActorMovie")]
        public async Task<IActionResult> DeleteDetailActorMovie(int Id)
        {
            var detail = await _unitOfWork.detailActorMovieRepository.FindToEntity(x => x.Id == Id);
            if (detail == null)
            {
                return NotFound();
            }
            var result = await _unitOfWork.detailActorMovieRepository.Delete(detail);
            if (result)
            {
                return Ok("Delete success");
            }
            return BadRequest("Delete failed");
        }

    }
}
