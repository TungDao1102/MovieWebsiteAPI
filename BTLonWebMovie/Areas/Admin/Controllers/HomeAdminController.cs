using Azure;
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
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listMovie = services.getAllMovieView().OrderBy(x => x.MovieName);
            PagedList<MovieView> lst = new PagedList<MovieView>(listMovie, pageNumber, pageSize);
            return View(lst);
        }

        [HttpGet]
        public IActionResult DetailMovie(int movieId)
        {
            var movie = services.getMovieById(movieId);
            return View(movie);
        }

        [HttpGet]
        public IActionResult CreateMovie()
        {
            ViewBag.TypeId = new SelectList(services.getAllType(), "TypeId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult CreateMovie(MovieView movie)
        {
            movie.IsDelete = false;
            var result = services.createMovie(movie);
            if (result)
            {
                return RedirectToAction("MovieCatalog");
            }
            ViewBag.TypeId = new SelectList(services.getAllType(), "TypeId", "Name");
            return View(movie);
        }

        [HttpGet]
        public IActionResult EditMovie(int movieId)
        {
            ViewBag.TypeId = new SelectList(services.getAllType(), "TypeId", "Name");
            var movie = services.getMovieById(movieId);
            return View(movie);
        }

        [HttpPost]
        public IActionResult EditMovie(MovieView movie)
        {
            var result = services.editMovie(movie);
            if (result)
            {
                return RedirectToAction("MovieCatalog");
            }
            ViewBag.TypeId = new SelectList(services.getAllType(), "TypeId", "Name");
            return View(movie);
        }

        public IActionResult DeleteMovie(int movieId)
        {
            TempData["Message"] = "";
            var result = services.deleteMovie(movieId);
            if (result)
            {
                TempData["Message"] = "Phim này đã được xóa";
            }
            return RedirectToAction("MovieCatalog");
        }

        public IActionResult DeletedMovies(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listDeletedMovies = services.getDeletedMovieView().OrderBy(x => x.MovieName);
            PagedList<MovieView> lst = new PagedList<MovieView>(listDeletedMovies, pageNumber, pageSize);
            return View(lst);
        }

        public IActionResult RestoreMovie(int movieId)
        {
            var result = services.restoreMovie(movieId);
            if (result)
            {
                TempData["MessageRestore"] = "Phim này đã được khôi phục";
            }
            return RedirectToAction("DeletedMovies");
        }

        public IActionResult Revenues(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listBill = services.getAllBill();
            PagedList<BillView> lst = new PagedList<BillView>(listBill, pageNumber, pageSize);
            return View(lst);
        }


    }
}
