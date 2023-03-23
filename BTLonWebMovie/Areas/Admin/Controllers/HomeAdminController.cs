using Microsoft.AspNetCore.Mvc;

namespace BTLonWebMovie.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
