using APIWebMovie.Models;
using BTLonWebMovie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Razorpay.Api;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BTLonWebMovie.Controllers
{
    public class PaymentController : Controller
    {
        private readonly RegistrationModel _registration;

        public PaymentController(RegistrationModel registration)
        {
            _registration = registration;
        }
       
        MovieWebContext db = new MovieWebContext();
        private const string _key = "rzp_test_Feks6hhqmGYnpC";
        private const string _secret = "fijpRGpWt5oj0lMg719IgYdo";

        public ViewResult Registration()
        {
			var model = new RegistrationModel() { Amount = 500};
			
			return View(model);
        }
        [HttpPost]
        public ViewResult Payment(RegistrationModel registration)
        {
            OrderModel order = new OrderModel()
            {
                OrderAmount = registration.Amount,
                Currency = "INR",
                Payment_Capture = 1, // 0 - Manual capture, 1 - Auto capture
                Notes = new Dictionary<string, string>()
                {
                    {"note 1", "first note while creating order " }, {"note 2", "can max 15 notes" }
                }
            };
            var oderId = CreateOrder(order);
            TempData["payment_capture"] = order.Payment_Capture;
            RazorPayOptionsModel razorPayOptions = new RazorPayOptionsModel()
            {
                Key = _key,
                AmountInSubUnits = order.OrderAmountInSubUnits,
                Currency = order.Currency,
                Name = "Thanh Toan Api",
                Description = "By Tung Dao",
                ImageLogUrl = "",
                OrderId = oderId,
                ProfileName = registration.Name,
                ProfileContact = registration.Mobile,
                ProfileEmail = registration.Email,
                Notes = new Dictionary<string, string>()
                {
                    {"note 1", "day la thanh toan mo ta 1  " }, {"note 2", "day la mo ta 2" }
                }
            };
            //  var name = _registration.Name;
            ViewBag.mobile = registration.Mobile;
            ViewBag.email = registration.Email;
            TempData["mobile"] = ViewBag.mobile;
            TempData["email"] = ViewBag.email;
            return View(razorPayOptions);
        }
        private string CreateOrder(OrderModel order)
        {
            try
            {
                RazorpayClient client = new RazorpayClient(_key, _secret);
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", order.OrderAmountInSubUnits); // so tien giao dich
                options.Add("currency", order.Currency); // loai tien te duoc giao dich
                options.Add("payment_capture", order.Payment_Capture);
                options.Add("notes", order.Notes);

                Order orderResponse = client.Order.Create(options);
                var orderId = orderResponse["id"].ToString();
                return orderId;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

      //  luu tru api va  so sanh signature
        public ViewResult AfterPayment()
        {
            var paymentStatus = Request.Form["paymentstatus"].ToString();
            if (paymentStatus == "Fail")
            {
                ViewBag.Message = "Oops! Something went wrong! Please contact helpdesk or try again later!";
                return View("Fail");
            }      
            var orderId = Request.Form["orderid"].ToString();
            var paymentId = Request.Form["paymentid"].ToString();
            var signature = Request.Form["signature"].ToString();

            var validSignature = CompareSignatures(orderId, paymentId, signature);
            if (validSignature)
            {
                ViewBag.Message = "Congratulations!! Your payment was successful";
                var paymentCapture = (int)TempData.Peek("payment_capture");
                if (paymentCapture == 1)
                {
                    AddBill(paymentId, orderId);
                    UpdateUser(1);
                }
                else
                {
                    TempData["orderid"] = orderId;
                }
                return View("Success");
            }
            else
            {
                ViewBag.Message = "Oops! Something went wrong! Please contact helpdesk or try again later!";
                return View("Fail");
            }

        }
        private bool CompareSignatures(string orderId, string paymentId, string razorPaySignature)
        {
            var text = orderId + "|" + paymentId;
            var secret = _secret;
            var generatedSignature = CalculateSHA256(text, secret);
            if (generatedSignature == razorPaySignature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string CalculateSHA256(string text, string secret)
        {
            string result = "";
            var enc = Encoding.Default;
            byte[]
            baText2BeHashed = enc.GetBytes(text),
            baSalt = enc.GetBytes(secret);
            System.Security.Cryptography.HMACSHA256 hasher = new HMACSHA256(baSalt);
            byte[] baHashedText = hasher.ComputeHash(baText2BeHashed);
            result = string.Join("", baHashedText.ToList().Select(b => b.ToString("x2")).ToArray());
            return result;
        }
        public IActionResult Capture()
        {
            return View();
        }
        public ViewResult CapturePayment(string paymentId)
        {
            RazorpayClient client = new RazorpayClient(_key, _secret);
            Payment payment = client.Payment.Fetch(paymentId);
            var amount = payment.Attributes["amount"];
            var currency = payment.Attributes["currency"];
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", amount);
            options.Add("currency", currency);
            Payment paymentCaptured = payment.Capture(options);
            ViewBag.Message = "Payment Captured!";
            var paymentCapture = (int)TempData.Peek("payment_capture");
            var orderId = TempData["orderid"] as string;
            var paymentid = Request.Form["paymentid"].ToString();
            AddBill(paymentid, orderId);
            UpdateUser(1);
            return View("Success");
        }
        [ValidateAntiForgeryToken]
        public void UpdateUser(int usertype)
        {
            string temp = TempData["UserId"] as string;
            TempData.Keep("UserId");
            int userId = Convert.ToInt32(temp);
            var user = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
            if (user != null)
            {
                user.UserType = usertype;
                db.SaveChanges();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void AddBill(string paymentId, string orderId)
        {
            //  var mobile = _registration.Mobile;
            //  var email = _registration.Email;
            var mobile = TempData["mobile"] as string;
            var email = TempData["email"] as string;
            string temp = TempData["UserId"] as string;
            DateTime now = DateTime.Now;
            int countdownDays = 30;
            int userid = Convert.ToInt32(temp);
          //  var user = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
            Bill bill = new Bill
            {
                UserId = userid,
                PaymentId = paymentId,
                OrderId = orderId,
                Amount = countdownDays+"" ,
                Email = email,
                Number = mobile,
                TimePayment = now,
                Status = null
            };
            db.Bills.Add(bill);
            db.SaveChanges();
        }
    }
}
