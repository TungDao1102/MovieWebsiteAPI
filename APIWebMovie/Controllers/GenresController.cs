using APIWebMovie.Controllers;
using APIWebMovie.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;

namespace APIMovieWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GenresController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllGenres")]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = _unitOfWork.genresRepository.FindToList<GenresView>(x => !x.IsDelete, x => x.GenresName, null);
            if (genres == null)
            {
                return NotFound();
            }
            return Ok(await genres);
        }

        [HttpGet("GetGenresById")]
        public async Task<IActionResult> GetGenresById(int genreId)
        {
            var genre = await _unitOfWork.genresRepository.Find<GenresView>(x => x.GenresId == genreId && !x.IsDelete);
            if (genre == null)
            {
                return NotFound("Genre not found");
            }
            return Ok(genre);
        }

        [HttpPut("EditGenre")]
        public async Task<IActionResult> EditGenre(GenresView genresView)
        {
            var genre = await _unitOfWork.genresRepository.FindToEntity(x => x.GenresId == genresView.GenresId);
            if (genre == null)
            {
                return NotFound();
            }
            genre.GenresName = genresView.GenresName;
            genre.IsDelete = genresView.IsDelete;
            var result = await _unitOfWork.genresRepository.Update(genre);
            if (result)
            {
                return Ok("Edit success");
            }
            return BadRequest("Edit failed");
        }

        [HttpDelete("DeleteGenres")]
        public async Task<IActionResult> DeleteGenres(int genreId)
        {
            var genre = await _unitOfWork.genresRepository.FindToEntity(x => x.GenresId == genreId && !x.IsDelete);
            if (genre == null)
            {
                return NotFound("Genre not found");
            }
            genre.IsDelete = true;
            var result = await _unitOfWork.genresRepository.Update(genre);
            if (result)
            {
                return Ok("Delete success");
            }
            return BadRequest("Delete success");
        }

        [HttpGet("GetGenresByMovieId")]

        public async Task<IActionResult> GetGenresByMovieId(int movieId)
        {
            var details = await _unitOfWork.detailGenresMovieRepository.FindToList<DetailGenresView>(x => x.MovieId == movieId);
            if (details == null)
            {
                return NotFound();
            }
            var listGenres = new List<GenresView>();
            foreach (var detail in details)
            {
                var genre = await _unitOfWork.genresRepository.Find<GenresView>(x => x.GenresId == detail.GenresId && !x.IsDelete);
                listGenres.Add(genre);
            }
            if (listGenres.Count > 0)
            {
                return Ok(listGenres);
            }
            return NotFound();
        }
    }
}
