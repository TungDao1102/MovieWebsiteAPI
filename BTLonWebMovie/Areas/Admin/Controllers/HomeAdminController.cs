using Azure;
using BTLonWebMovie.Services.API;
using Microsoft.AspNetCore.Http;
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
            return View(movie);
        }

        [HttpGet]
        public IActionResult EditMovie(int movieId)
        {

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

        public IActionResult UserManagement(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listUser = services.getAllUserView();
            PagedList<UserView> lst = new PagedList<UserView>(listUser, pageNumber, pageSize);
            return View(lst);
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(UserView user)
        {
            user.IsVerify = true;
            var result = services.createUser(user);
            if (result)
            {
                return RedirectToAction("UserManagement");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult EditUser(int userId)
        {
            var user = services.getUserById(userId);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserView user, IFormFile file)
        {
            TempData["MessagUpdate"] = "";
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
                var resultUpdate = services.UpdateAvatar(pathAfterCombine);
                user.Avatar = resultUpdate;
            }
            else
            {
                user.Avatar = "Unknown";
            }

            var result = services.editUser(user);
            if (result)
            {
                return RedirectToAction("UserManagement");
            }
            return View(user);
        }

        public IActionResult DeleteUser(int userId)
        {
            TempData["MessageDeleteUser"] = "";
            var result = services.deleteUser(userId);
            if (result)
            {
                TempData["MessageDeleteUser"] = "Người dùng này đã được xóa";
            }
            return RedirectToAction("UserManagement");
        }

    }
}
