using BTLonWebMovie.Models;
using Microsoft.AspNetCore.Mvc;
namespace BTLonWebMovie.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            if (TestLogin.Role == 1)
            {
                TempData["Error"] = null;
                return View();
            }
            else
            {
                //   ModelState.AddModelError("", "Bạn không có quyền truy cập");
                TempData["Error"] = "Bạn không có quyền truy cập";
                return RedirectToAction("Error");
            }
        }
        public IActionResult Error()
        {
            ViewBag.ErrorMessage = TempData["Error"] as string;
            return View();
        }
    }
}
