using APIWebMovie.Models;
using Microsoft.AspNetCore.Mvc;

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
					HttpContext.Session.SetString("UserName", u.UserName.ToString());
					return RedirectToAction("Index", "Home");
				}
			}
			return View();
		}
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("UserName");
			return RedirectToAction("Login", "Access");
		}
	}
}
