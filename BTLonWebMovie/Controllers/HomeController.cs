using Azure;
using BTLonWebMovie.Models;
using BTLonWebMovie.Models.Authentication;
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
       
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }
        [Authentication]
        public IActionResult Index()
        {
            HttpClient client = _factory.CreateClient("myclient");
            var response = client.GetAsync("/api/Movie/GetAllMovie").Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<List<MovieView>>(jsonData);
            ViewBag.UserRole = TempData["UserRole"] as string;
            return View(data);
        }
        public IActionResult PlayMovie(int id)
        {
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

        
    }
}