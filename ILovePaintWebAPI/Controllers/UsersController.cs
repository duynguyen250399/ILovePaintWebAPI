using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Models;
using ILovePaintWebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.UserService;

namespace ILovePaintWebAPI.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult AuthenticateUser(AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect!" });
            }

            Authentication auth = new Authentication(_configuration);

            string tokenString = auth.GenerateJwtToken(user);

            return Ok(new
            {
                ID = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Token = tokenString
            });
        }

        [HttpGet]  
        public IActionResult GetUser()
        {
            var user = _userService.GetAllUsers();

            if(user == null)
            {
                return NotFound(new { message = "Users not found!" });
            }

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(UserRegistrationModel model)
        {
            if(model == null)
            {
                return BadRequest(new
                {
                    message = "Registration data is null!"
                });
            }

            var existingUser = await _userManager.FindByNameAsync(model.Username);
            if(existingUser != null)
            {
                return BadRequest(new { message = "This username is already taken!"});
            }
            existingUser = await _userManager.FindByEmailAsync(model.Email);
            if(existingUser != null)
            {
                return BadRequest(new { message = "This email is already taken!" });
            }

            var userEntity = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Birthdate = model.Birthdate,
                Address = model.Address,
                EmailConfirmed = false
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
        [AllowAnonymous]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
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
                return Ok(new { message = "Confirm account successfully!" });
            }

            return Content("Failed to confirm account!");
        }

    }
}