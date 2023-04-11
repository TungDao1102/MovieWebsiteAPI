using APIWebMovie.Helper;
using APIWebMovie.Interface;
using APIWebMovie.Models;
using APIWebMovie.Services;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using ModelAccess.ViewModel;

namespace APIWebMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailServices _services;
        public UserController(IUnitOfWork unitOfWork, IEmailServices services)
        {
            _unitOfWork = unitOfWork;
            _services = services;
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _unitOfWork.userRepository.FindToList<UserView>(x => !x.IsDelete);
            return Ok(users);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserView userView)
        {
            var result = await _unitOfWork.userRepository.Add<UserView>(userView);
            if (result)
            {
                return Ok("Add success");
            }
            return BadRequest("Add failed");
        }

        [HttpPut("EditUser")]
        public async Task<IActionResult> EditUser(UserView view)
        {
            var user = await _unitOfWork.userRepository.FindToEntity(x => x.UserId == view.UserId);
            if (user == null)
            {
                return NotFound();
            }
            user.UserName = view.UserName;
            user.Email = view.Email;
            user.PassWord = view.Password;
            user.UserType = view.UserType;
            user.IsVerify = view.IsVerify;
            user.Avatar = view.Avatar; 
            var result = await _unitOfWork.userRepository.Update(user);
            if (result)
            {
                return Ok("Update success");
            }
            return BadRequest("Update failed");
        }

        [HttpGet]
        [Route("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string Email)
        {
            var user = await _unitOfWork.userRepository.Find<UserView>(x => x.Email == Email);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int UserId)
        {
            var user = await _unitOfWork.userRepository.GetById<UserView>(UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser(UserView register)
        {
            var userExist = await _unitOfWork.userRepository.Find<UserView>(x => x.Email == register.Email && !x.IsDelete);
            if (userExist != null)
            {
                return BadRequest("User exist");
            }
            register.Avatar = "Unknown";
            var result = await _unitOfWork.userRepository.Add<UserView>(register);
            if (result)
            {
                return Ok("Register success");
            }
            return BadRequest("Register failed");
        }

        [HttpPost]
        [Route("LoginUser")]

        public async Task<IActionResult> Login(UserView login)
        {
            var user = await _unitOfWork.userRepository.Find<UserView>(x => x.Email == login.Email && x.IsVerify);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (login.Password != user.Password)
            {
                return BadRequest("Password wrong");
            }
            HttpContext.Session.SetString("UserID", user.UserId.ToString());
            return Ok("Login success");
        }

        [HttpPost]
        [Route("LogoutUser")]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Remove("UserID");
                return Ok("Logout success");
            }
            catch
            {
                return BadRequest("Logout failed");
            }
        }

        [HttpGet]
        [Route("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userID = HttpContext.Session.GetString("UserID");
            if (userID != null)
            {
                try
                {
                    var user = await _unitOfWork.userRepository.GetById<UserView>(int.Parse(userID));
                    return Ok(user);
                }
                catch
                {
                    return BadRequest("Error");
                }
            }
            return BadRequest("Not found");
        }

        [HttpPost]
        [Route("SendOtp")]
        public IActionResult SendOtp(SendEmail send)
        {
            if (send.Otp != 0)
            {
                send.Content += " " + send.Otp;
            }
            var message = new Message(new string[] { send.Email }, send.Title, send.Content);
            _services.SendEmail(message);
            return Ok("Send success");
        }

        [HttpPut]
        [Route("VerifyUser")]
        public async Task<IActionResult> VerifyUser(string Email)
        {

            var userExist = await _unitOfWork.userRepository.FindToEntity(x => x.Email == Email);
            if (userExist == null)
            {
                return NotFound("User not found");
            }
            userExist.IsVerify = true;
            var result = await _unitOfWork.userRepository.Update(userExist);
            if (result)
            {
                return Ok("Update success");
            }
            return BadRequest("Update failed");
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userExist = await _unitOfWork.userRepository.FindToEntity(x => x.UserId == id);
            if (userExist == null)
            {
                return NotFound();
            }
            userExist.IsDelete = true;
            var result = await _unitOfWork.userRepository.Update(userExist);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest("Delete failed");
        }

        [HttpGet]
        [Route("UpdateAvatar")]
        public async Task<IActionResult> UpdateAvatar(string pathFile)
        {
            string path = pathFile;
            var stream = new FileStream(path, FileMode.Open);
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(Util.ApiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(Util.AuthEmail, Util.AuthPassword);
                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    Util.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("UserUpload")
                    .Child(DateTime.Now.Ticks.ToString())
                    .PutAsync(stream, cancellation.Token);
                return StatusCode(StatusCodes.Status200OK, await task);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed");
            }
            finally
            {
                stream.Dispose();
                stream.Close();
            }
        }
    }
}
