using APIWebMovie.Models;
using Azure;
using BTLonWebMovie.Models;
using BTLonWebMovie.Models.Authentication;
using BTLonWebMovie.Services.API;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

namespace BTLonWebMovie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _factory;
        private readonly APIServices _services;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory factory)
        {
            _logger = logger;
            _factory = factory;
            _services = new APIServices(_factory);
        }
        [Authentication]
        public IActionResult Index()
        {
            var data = _services.getAllMovieView();
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            return View(data);
        }

        public IActionResult searchByGenres(int genresId) {
            var genres = _services.searchMovieByGenres(genresId);
            TempData["genres"] = JsonConvert.SerializeObject(genres);
            return RedirectToAction("searchView","Home");
        }

        public IActionResult searchByNameOrActor(string name)
        {
            var movies = _services.searchMovieByNameOrActor(name);
            TempData["movies"] = JsonConvert.SerializeObject(movies);
            return RedirectToAction("searchView", "Home");
        }

        public IActionResult searchView()
        {
            List<MovieView> movies = null;
            if(TempData["genres"].ToString() != null)
            {
                movies = JsonConvert.DeserializeObject<List<MovieView>>(TempData["genres"].ToString());
            } 
            else if(TempData["movies"].ToString() != null)
            {
                movies = JsonConvert.DeserializeObject<List<MovieView>>(TempData["movies"].ToString());
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