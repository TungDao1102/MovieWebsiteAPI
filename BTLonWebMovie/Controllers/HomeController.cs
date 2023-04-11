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
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var user = _services.getUserById(userId);
            ViewBag.Avatar = user.Avatar;
            return View(data);
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
                userUpdate.Avatar = "Unknown";
            }
            userUpdate.UserName = user.UserName;
            userUpdate.Password = user.Password;
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