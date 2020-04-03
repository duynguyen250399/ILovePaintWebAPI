using DataLayer.Entities;
using DataLayer.Models;
using ILovePaintWebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServiceLayer.UserService;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ILovePaintWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<User> _userManager;

        public UsersController(IUserService userService,
            IConfiguration configuration,
            IWebHostEnvironment env,
            UserManager<User> userManager)
        {
            _userService = userService;
            _configuration = configuration;
            _userManager = userManager;
            _env = env;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> UserLogin(AuthenticateModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return BadRequest(new { message = "Incorrect username or password!" });
            }

            var validUser = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!validUser)
            {
                return BadRequest(new { message = "Incorrect username or password!" });
            }

            if (!user.EmailConfirmed)
            {
                return BadRequest(new { status = "Login failed", message = "This account is not activated yet" });
            }

            Authentication auth = new Authentication(_configuration, _userManager);

            string tokenString = await auth.GenerateJwtToken(user);

            return Ok(new
            {
                token = tokenString
            });
        }

        [HttpGet]
        [Route("profile")]
        [Authorize(Roles = "Member,Admin")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { message = "User not found!" });
            }

            return Ok(new
            {
                FullName = user.FullName,
                Gender = user.Gender,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                RewardPoints = user.RewardPoints,
                Image = user.Image == null ? null : _configuration["backendEnv:host"] + "/api/users/images/" + user.Id
            });
        }


        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegistrationModel model)
        {
            if (model == null)
            {
                return BadRequest(new
                {
                    message = "Registration data is null!"
                });
            }

            var existingUser = await _userManager.FindByNameAsync(model.Username);
            if (existingUser != null)
            {
                return BadRequest(new { message = "This username is already taken!" });
            }
            existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "This email is already taken!" });
            }

            var userEntity = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                EmailConfirmed = false,
                RewardPoints = 0
            };

            var addUserResult = await _userManager.CreateAsync(userEntity, model.Password);
            if (addUserResult.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(userEntity);
                var confirmationLink = Url.Action("ConfirmEmail", "Users", new { userId = userEntity.Id, token = token }, Request.Scheme);

                EmailHandler emailHandler = new EmailHandler(_env);
                emailHandler.SendAccountConfirmEmail(userEntity.Email, "ILovePaint - Account Confirmation", "Please click the link below to confirm your account", confirmationLink);

                var addRoleResult = await _userManager.AddToRoleAsync(userEntity, "Member");
                if (addRoleResult.Succeeded)
                {
                    model.Password = null;
                    return Ok(new
                    {
                        message = "Register user successfully!",
                        data = model
                    });
                }

                return Content("Failed to add user role!");
            }

            return Content("Failed to register account!");

        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { messgage = "User not found!" });
            }

            if (user.EmailConfirmed)
            {
                return NoContent();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return Redirect("http://localhost:4200");
            }

            return Content("Failed to confirm account!");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("admin")]
        public async Task<IActionResult> RegisterAdmin(UserRegistrationModel model)
        {
            if (model == null)
            {
                return BadRequest(new
                {
                    message = "Registration data is null!"
                });
            }

            var existingUser = await _userManager.FindByNameAsync(model.Username);
            if (existingUser != null)
            {
                return BadRequest(new { message = "This username is already taken!" });
            }
            existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "This email is already taken!" });
            }

            var userEntity = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                EmailConfirmed = true,
                RewardPoints = 0
            };

            var addUserResult = await _userManager.CreateAsync(userEntity, model.Password);
            if (addUserResult.Succeeded)
            {
                var addRoleResult = await _userManager.AddToRoleAsync(userEntity, "Admin");
                if (addRoleResult.Succeeded)
                {
                    model.Password = null;
                    return Ok(new
                    {
                        message = "Register admin user successfully!",
                        data = model
                    });
                }

                return Content("Failed to add admin role!");
            }

            return Content("Failed to register admin account!");

        }

        [HttpGet]
        [Route("profile/admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminInfo()
        {
            var admin = await _userManager.FindByNameAsync("admin");
            if (admin == null)
            {
                return NotFound(new { messgage = "Admin not found!" });
            }

            return Ok(admin);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> UpdateUserProfile([FromForm] UserProfileModel model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Profile is null!" });
            }

            if (string.IsNullOrEmpty(model.UserID))
            {
                return BadRequest(new { message = "Can not find user profile!" });
            }

            var user = await _userManager.FindByIdAsync(model.UserID);

            if (user == null)
            {
                return BadRequest(new { message = "Can not find user!" });
            }

            user.FullName = model.FullName;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            user.Gender = model.Gender;

            // process user avatar upload
            if (model.Avatar != null && model.Avatar.Length > 0)
            {

                // delete old avatar file if exists
                if (!string.IsNullOrEmpty(user.Image))
                {
                    string pathToDelete = _env.WebRootPath + user.Image.Replace("/", "\\");
                    if (System.IO.File.Exists(pathToDelete))
                    {
                        System.IO.File.Delete(pathToDelete);
                    }
                }

                // generate image file name
                var uuid = System.Guid.NewGuid().ToString();
                string path = @"/uploads/images/users/" + "iLovePaint-"
                    + model.UserID + "-"
                    + uuid + "-" + model.Avatar.FileName;
                user.Image = path;

                string uploadPath = path.Replace("/", "\\");

                // create new directory if not exist
                if (!Directory.Exists(_env.WebRootPath + @"\uploads\images\users\"))
                {
                    Directory.CreateDirectory(_env.WebRootPath + @"\uploads\images\users\");
                }

                using (var stream = System.IO.File.Create(_env.WebRootPath + uploadPath))
                {
                    await model.Avatar.CopyToAsync(stream);
                    await stream.FlushAsync();
                }
            }



            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    status = "success",
                    message = "user profile updated",
                    model = model
                }); ;
            }
            else
            {
                return BadRequest(new { message = "Error when updating profile!" });
            }
        }

        [HttpGet]
        [Route("images/{userID}")]
        public async Task<IActionResult> GetUserImage(string userID)
        {
            var user = await _userManager.FindByIdAsync(userID);
            if (user == null)
            {
                return NotFound(new { message = "User not found!" });
            }

            if (string.IsNullOrEmpty(user.Image))
            {
                return NotFound(new { message = "Image not found!" });
            }

            var path = _env.WebRootPath + user.Image.Replace("/", "\\");

            var avatarFile = System.IO.File.OpenRead(path);
            if (avatarFile == null)
            {
                return NotFound(new { message = "Image not found!" });
            }

            return File(avatarFile, "image/jpeg");
        }

    }
}