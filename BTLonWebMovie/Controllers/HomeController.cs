using Azure;
using BTLonWebMovie.Models;
using BTLonWebMovie.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
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
            List<Movie> data = new List<Movie>();
            HttpClient client = _factory.CreateClient("myclient");
            var response = client.GetAsync("/api/Movie").Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            data = JsonConvert.DeserializeObject<List<Movie>>(jsonData);
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