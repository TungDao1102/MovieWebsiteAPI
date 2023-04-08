using APIWebMovie.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;


namespace BTLonWebMovie.Controllers
{
	public class AccessController : Controller
	{
		MovieWebContext db = new MovieWebContext();
		[HttpGet]
		public IActionResult Login()
		{
			if (HttpContext.Session.GetString("UserName") == null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
		[HttpPost]
		public IActionResult Login(User user)
		{
			if (HttpContext.Session.GetString("UserName") == null)
			{
				var u = db.Users.Where(x =>x.UserName.Equals(user.UserName) && x.PassWord.Equals(user.PassWord)).FirstOrDefault();
				if (u != null)
				{
					 HttpContext.Session.SetString("UserId", u.UserId.ToString());
					HttpContext.Session.SetString("UserRole", u.UserType.ToString());
                    string userid = HttpContext.Session.GetString("UserId");
                    //	ViewBag.UserId = userid;
                    string userrole = HttpContext.Session.GetString("UserRole");
					TempData["UserRole"] = userrole;
                    TempData["UserId"] = userid;
                    HttpContext.Session.SetString("UserName", u.UserName.ToString());
					return RedirectToAction("Index", "Home");
				}
			}
			return View();
		}
        //[HttpGet]
        //public IActionResult GetSessionData()
        //{
        // //   string userId = HttpContext.Session.GetString("UserId");
        //    string userRole = HttpContext.Session.GetString("UserRole");
        //    return Json(new { userRole = userRole });
        //}

        public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("UserName");
			return RedirectToAction("Login", "Access");
		}
	}
}
