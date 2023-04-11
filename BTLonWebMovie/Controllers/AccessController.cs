using APIWebMovie.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;
using ModelAccess.ViewModel;
using BTLonWebMovie.Services.API;

namespace BTLonWebMovie.Controllers
{
    public class AccessController : Controller
    {
        MovieWebContext db = new MovieWebContext();
        private readonly IHttpClientFactory _factory;
        private readonly APIServices _services;
        public AccessController(IHttpClientFactory factory)
        {
            _factory = factory;
            _services = new APIServices(_factory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserView userView)
        {
            var result = _services.RegisterUser(userView);
            if (result)
            {
                Random random = new Random();
                long otp = random.Next(100000, 1000000);
                TempData["Email"] = userView.Email;
                TempData["Otp"] = otp.ToString();
                var sendmail = new SendEmail()
                {
                    Email = userView.Email,
                    Title = "MovieWeb",
                    Content = "Hi " + userView.UserName +", Welcome to Movie, Your OTP: ",
                    Otp = otp
                };                
                var resultSendMail = _services.SendEmail(sendmail);
                if (resultSendMail)
                {
                    return RedirectToAction("Verify");
                }
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
        }


        public IActionResult Verify()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Verify(long Otp)
        {
            long OtpAdmin = long.Parse((string)TempData["Otp"]);
            TempData.Keep("Otp");
            if (OtpAdmin == Otp)
            {
                var email = TempData["Email"] as string;
                TempData.Keep("Email");
                var result = _services.VerifyUser(email);
                if (result)
                {
                    return RedirectToAction("Login");
                }
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserView user)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                var u = db.Users.Where(x => x.Email.Equals(user.Email) && x.PassWord.Equals(user.Password)).FirstOrDefault();
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
                    HttpContext.Session.SetString("Email", u.Email.ToString());
                    if(u.UserType == 2)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.Error = "Email or Password wrong";
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
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Login", "Access");
        }
    }
}