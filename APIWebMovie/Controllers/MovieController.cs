using APIWebMovie.Interface;
using APIWebMovie.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;

namespace APIWebMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public MovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllMovie")]
        public async Task<IActionResult> GetAllMovie()
        {
            var movies = await _unitOfWork.movieRepository.FindToList<MovieView>(x => !x.IsDelete);
            return Ok(movies);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _unitOfWork.movieRepository.FindToList<MovieView>(x => x.MovieId == id && !x.IsDelete);
            if (movie == null)
            {
                return NotFound("Movie not found");
            }
            return Ok(movie);
        }

        [HttpPut("UpdateViewMovie")]
        public async Task<IActionResult> UpdateViewMovie(int MovieId)
        {
            var user = await _unitOfWork.movieRepository.FindToEntity(x => x.MovieId == MovieId);
            if (user == null)
            {
                return NotFound("Movie not found");
            }
            user.ViewCount++;
            var result = await _unitOfWork.movieRepository.Update(user);
            if (result)
            {
                return Ok("Update viewCount success");
            }
            return BadRequest("Update viewCount failed");
        }

        [HttpGet("GetTopMovie")]
        public async Task<IActionResult> GetTopMovie(int quantity)
        {
            var movies = await _unitOfWork.movieRepository.FindToList<MovieView>(x => !x.IsDelete, x => x.ViewCount!, quantity);
            if (movies == null)
            {
                return NotFound();
            }
            return Ok(movies);
        }

        [HttpGet("GetMovieByQuantity")]
        public async Task<IActionResult> GetMovieByQuantity(int quantity)
        {
            var list = await _unitOfWork.movieRepository.FindToList<MovieView>(x => !x.IsDelete, x => x.MovieName, quantity);
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        [HttpGet("SearchMovieByGenre")]
        public async Task<IActionResult> SearchMovieByGenre(int genreId)
        {
            var details = await _unitOfWork.detailGenresMovieRepository.FindToList<DetailGenresView>(x => x.GenresId == genreId);
            if (details == null)
            {
                return NotFound();
            }
            var movies = new List<MovieView>();
            foreach (var item in details)
            {
                var movieView = await _unitOfWork.movieRepository.Find<MovieView>(x => x.MovieId == item.MovieId && !x.IsDelete);
                movies.Add(movieView);
            }
            return Ok(movies);
        }

        [HttpGet("SearchMovieByActor")]
        public async Task<IActionResult> SearchMovieByActor(string nameActor)
        {
            var actor = await _unitOfWork.actorRepository.Find<ActorView>(x => x.ActorName == nameActor && !x.IsDelete);
            if (actor == null)
            {
                return NotFound();
            }

            var movies = new List<MovieView>();
            var details = await _unitOfWork.detailActorMovieRepository.FindToList<DetailActorView>(x => x.ActorId == actor.ActorId);
            foreach (var detail in details)
            {
                var movie = await _unitOfWork.movieRepository.Find<MovieView>(x => !x.IsDelete && x.MovieId == detail.MovieId);
                movies.Add(movie);
            }
            return Ok(movies);
        }

        [HttpGet("SearchMovieByName")]
        public async Task<IActionResult> SearchMovieByName(string nameMovie)
        {
            var movies = await _unitOfWork.movieRepository.FindToList<MovieView>(x => x.MovieName.Contains(nameMovie) && !x.IsDelete, null, null);
            if (movies == null)
            {
                return NotFound();
            }
            return Ok(movies);
        }

        [HttpDelete("DeleteMovie")]
        public async Task<IActionResult> DeleteMovie(int MovieId)
        {
            var movie = await _unitOfWork.movieRepository.FindToEntity(x => x.MovieId == MovieId && !x.IsDelete);
            if (movie == null)
            {
                return NotFound();
            }
            movie.IsDelete = true;
            var result = await _unitOfWork.movieRepository.Update(movie);
            if (result)
            {
                return Ok("Delete success");
            }
            return BadRequest("Delete failed");
        }

        [HttpPut("EditMovie")]
        public async Task<IActionResult> EditMovie(MovieView view)
        {
            var movie = await _unitOfWork.movieRepository.FindToEntity(x => x.MovieId == view.MovieId && !x.IsDelete);
            if (movie == null)
            {
                return NotFound();
            }
            movie.MovieName = view.MovieName;
            movie.TypeId = view.TypeId;
            movie.OverView = view.OverView;
            movie.AverageRating = view.AverageRating;
            movie.ViewCount = view.ViewCount;
            movie.ReleaseDate = view.ReleaseDate;
            movie.PosterPath = view.PosterPath;
            movie.Duration = view.Duration;
            movie.IsDelete = view.IsDelete;
            var result = await _unitOfWork.movieRepository.Update(movie);
            if (result)
            {
                return Ok("Update success");
            }
            return BadRequest("Update failed");
        }

        [HttpGet("CheckMovieVip")]
        public async Task<IActionResult> CheckMovieVip(int movieId)
        {
            var movie = await _unitOfWork.movieRepository.Find<MovieView>(x => x.MovieId == movieId && !x.IsDelete);
            if (movie == null)
            {
                return NotFound();
            }
            bool check = false;
            if (movie.TypeId == 1)
            {
                check = false;
            }
            else
            {
                check = true;
            }
            return Ok(check);
        }

        [HttpGet("GetReviewByMovie")]
        public async Task<IActionResult> GetReviewByMovie(int movieId)
        {
            var reviews = await _unitOfWork.reviewRepository.FindToList<ReviewView>(x => x.MovieId == movieId);
            if (reviews == null)
            {
                return NotFound();
            }
            return Ok(reviews);
        }
    }
}
