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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.UserService;

namespace ILovePaintWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
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
                ID = user.ID,
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

    }
}