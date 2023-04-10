using APIWebMovie.Helper;
using APIWebMovie.Interface;
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
            var userExist = await _unitOfWork.userRepository.Find<UserView>(x => x.Email == register.Email);
            if (userExist != null)
            {
                return BadRequest("User exist");
            }
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
            var result = await _unitOfWork.userRepository.Delete(userExist);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest("Delete failed");
        }

        [HttpPut("UpdateAvatar")]
        public async Task<IActionResult> UpdateAvatar(string pathFile, int UserId)
        {
            var stream = new FileStream(pathFile, FileMode.Open);
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
                    .Child("AvatarUser")
                    .Child(UserId.ToString() + "_" + DateTime.Now.Ticks.ToString())
                    .PutAsync(stream, cancellation.Token);
                var user = await _unitOfWork.userRepository.FindToEntity(x => x.UserId == UserId);
                if (user == null)
                {
                    return NotFound();
                }
                user.Avatar = await task;
                var result = await _unitOfWork.userRepository.Update(user);
                if (result)
                {
                    return Ok(task);
                }
                return BadRequest("Update failed");
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
