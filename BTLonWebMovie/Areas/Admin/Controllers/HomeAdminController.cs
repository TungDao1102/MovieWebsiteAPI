using BTLonWebMovie.Models;
using BTLonWebMovie.Services.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModelAccess.ViewModel;
using X.PagedList;

namespace BTLonWebMovie.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeAdminController : Controller
    {
  
        private readonly ILogger<HomeAdminController> _logger;
        private APIServices services;
        public HomeAdminController(ILogger<HomeAdminController> logger, IHttpClientFactory factory)
        {
            _logger = logger;
            services = new APIServices(factory);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MovieCatalog(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null|| page < 0 ? 1 : page.Value;
            var listMovie = services.getAllMovieView().OrderBy(x => x.MovieName);
            PagedList<MovieView> lst = new PagedList<MovieView>(listMovie, pageNumber, pageSize);
            return View(lst);          
        }

        [HttpGet]
        public IActionResult CreateMovie()
        {
            ViewBag.TypeId = new SelectList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateMovie(MovieView movie)
        {
            var result = services.createMovie(movie);
            if (result)
            {
                return RedirectToAction("MovieCatalog");
            }
            return View(movie);
        }

        //[HttpPost]
        //public async Task<IActionResult> updateViewCount(int songId)
        //{
        //    var song = await _unitOfWork.songRepository.GetById(songId);
        //    if (song == null) { return NotFound(); }
        //    song.ViewCount++;
        //    var result = await _unitOfWork.songRepository.Update(song);
        //    if (result == 1)
        //    {
        //        return Ok("Update success");
        //    }
        //    return BadRequest("Update failed");
        //}

        //[HttpGet]
        //public IActionResult EditMovie(string movieID)
        //{
        //    ViewBag.TypeId = new SelectList(_context.TypeMovies.ToList(), "TypeId", "Name");
        //    var movie = _context.Movies.Find(movieID);
        //    return View(movie);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult EditMovie(Movie movie)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Update(movie);
        //        _context.SaveChanges();
        //        return RedirectToAction("MovieCatalog");
        //    }
        //    return View(movie);
        //}
    }
}
