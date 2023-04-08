using APIWebMovie.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;

namespace APIWebMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieFavoriteController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieFavoriteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetMovieFavorite")]
        public async Task<IActionResult> GetMovieFavorite(int UserId)
        {
            var details = await _unitOfWork.detailUserMovieFavoriteRepository.FindToList<MovieFavoriteView>(x => x.UserId == UserId);
            if (details == null)
            {
                return NotFound();
            }
            var movies = new List<MovieView>();
            foreach (var item in details)
            {
                var movieView = await _unitOfWork.movieRepository.Find<MovieView>(x => x.MovieId == item.MovieId);
                movies.Add(movieView);
            }
            return Ok(movies);
        }

        [HttpPost("AddMovieFavorite")]
        public async Task<IActionResult> AddMovieFavorite(MovieFavoriteView movieFavorite)
        {
            var result = await _unitOfWork.detailUserMovieFavoriteRepository.Add<MovieFavoriteView>(movieFavorite);
            if (result)
            {
                return Ok("Add movie favorite success");
            }
            return BadRequest("Add movie favorite failed");
        }

        [HttpDelete("DeleteMovieFavorite")]
        public async Task<IActionResult> DeleteMovieFavorite(MovieFavoriteView movieFavorite)
        {
            var detail = await _unitOfWork.detailUserMovieFavoriteRepository.FindToEntity(x => x.MovieId == movieFavorite.MovieId && x.UserId == movieFavorite.UserId);
            if(detail == null) { return NotFound("Detail not found"); }
            var result = await _unitOfWork.detailUserMovieFavoriteRepository.Delete(detail);
            if (result)
            {
                return Ok("Delete success");
            }
            return BadRequest("Delete failed");
        }
    }
}
