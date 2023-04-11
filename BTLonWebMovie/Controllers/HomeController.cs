using APIWebMovie.Models;
using Azure;
using BTLonWebMovie.Models;
using BTLonWebMovie.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text.Json;

namespace BTLonWebMovie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _factory;
       
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory factory)
        {
            _logger = logger;
            _factory = factory;
            _services = new APIServices(_factory);
        }
        [Authentication]
        public IActionResult Index()
        {
            var listMovie = _services.getAllMovieView();
            var listMovieViewModel = new List<MovieViewModel>();
            foreach (var item in listMovie)
            {
                var listActors = _services.getActorByMovie(item.MovieId);
               var listDirectors = _services.getDirectorByMovie(item.MovieId);
                var _listGenres = _services.getGenresByMovie(item.MovieId);
                var movieViewModel = new MovieViewModel
                {
                    movie = item,
                    listActor = listActors,
                    listDirector = listDirectors,
                    listGenres = _listGenres
                };
                listMovieViewModel.Add(movieViewModel);
            }
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            return View(listMovieViewModel);
        }

        public IActionResult searchByGenres(int genresId) {
            var genres = _services.searchMovieByGenres(genresId);
            TempData["genres"] = JsonConvert.SerializeObject(genres);
            return RedirectToAction("searchView","Home");
        }

        public IActionResult searchByNameOrActor(string name)
        {
            var movies = _services.searchMovieByNameOrActor(name);
            return View(movies);
        }

        public IActionResult searchView()
        {
            List<MovieView> movies = null;
            if(TempData["genres"].ToString() != null)
            {
                movies = JsonConvert.DeserializeObject<List<MovieView>>(TempData["genres"].ToString());
                TempData.Keep("genres");
            }            
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}