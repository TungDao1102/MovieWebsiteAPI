using APIWebMovie.Models;
using Azure;
using BTLonWebMovie.Helper;
using BTLonWebMovie.Models;
using BTLonWebMovie.Models.Authentication;
using BTLonWebMovie.Models.ViewModels;
using BTLonWebMovie.Services.API;
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
            var listMovie = _services.getAllMovieView();
            var listMovieViewModel = new List<MovieViewModel>();

            foreach (var item in listMovie)
            {
                var listActors = _services.getActorByMovie(item.MovieId);
                var listDirectors = _services.getDirectorByMovie(item.MovieId);
                var _listGenres = _services.getGenresByMovie(item.MovieId);
                var _topView = _services.getTopMovie(10);
                var movieViewModel = new MovieViewModel
                {
                    movie = item,
                    listActor = listActors,
                    listDirector = listDirectors,
                    listGenres = _listGenres,
                    topView = _topView
                };
                listMovieViewModel.Add(movieViewModel);
            }
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            SetMenuUser();

            return View(listMovieViewModel);
        }

        public IActionResult AddToFavorite(int movieid)
        {
            var moviefav = new MovieFavoriteView()
            {
                MovieId = movieid,
                UserId = int.Parse(HttpContext.Session.GetString("UserId"))
            };
         
            _services.AddMovieFavorite(moviefav);
            return RedirectToAction("MovieFavoriteView");
        }

        public IActionResult MovieFavoriteView()
        {
            return View();
        }
        public IActionResult UserDetail()
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var user = _services.getUserById(userId);
            return View(user);
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var user = _services.getUserById(id);
            HttpContext.Session.SetString("Avatar", user.Avatar);
            HttpContext.Session.SetString("Password", user.Password);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserView user, IFormFile file)
        {
            var userUpdate = _services.getUserById(user.UserId);
            if (file != null)
            {
                string path = Path.Combine(Environment.CurrentDirectory, "UploadedFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    fileStream.Dispose();
                    fileStream.Close();
                }
                var pathAfterCombine = Path.Combine(Environment.CurrentDirectory, "UploadedFile", file.FileName);
                var resultUpdate = _services.UpdateAvatar(pathAfterCombine);
                userUpdate.Avatar = resultUpdate;
            }
            else
            {
                userUpdate.Avatar = HttpContext.Session.GetString("Avatar");
                HttpContext.Session.Remove("Avatar");
            }
            if (HttpContext.Session.GetString("Password") != user.Password.Trim())
            {
                var encryptPassword = XString.ToMD5(user.Password.Trim());
                userUpdate.Password = encryptPassword;
            }
            userUpdate.UserName = user.UserName;            
            var result = _services.editUser(userUpdate);
            if (result)
            {
                return RedirectToAction("UserDetail");
            }
            return View(user);
        }



        public IActionResult PlayMovie(int id)
        {
            return View();
        }
        public IActionResult searchByGenres(int genresId)
        {
            var genres = _services.searchMovieByGenres(genresId);
            TempData["genres"] = JsonConvert.SerializeObject(genres);
            return RedirectToAction("searchView", "Home");
        }

        public IActionResult searchByNameOrActor(string name)
        {
            SetMenuUser();
            var movies = _services.searchMovieByNameOrActor(name);
            var listMovieViewModel = new List<MovieViewModel>();
            foreach (var item in movies)
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

            return View(listMovieViewModel);

        }

        public IActionResult searchView()
        {
            SetMenuUser();
            if (TempData["genres"].ToString() != null)
            {
                var movies = JsonConvert.DeserializeObject<List<MovieView>>(TempData["genres"].ToString());
                TempData.Keep("genres");
                var listMovieViewModel = new List<MovieViewModel>();
                foreach (var item in movies)
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
                return View(listMovieViewModel);
            }
            return View();
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

        public void SetMenuUser()
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var user = _services.getUserById(userId);
            ViewBag.Avatar = user.Avatar;
        }



    }
}