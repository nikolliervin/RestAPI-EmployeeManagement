using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestAPI_Kreatx.Data;
using RestAPI_Kreatx.Infrastructure;
using RestAPI_Kreatx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RestAPI_Kreatx.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _config;
        private UserManager<APIUser> _userManager;
        private APIIdentityContext _identity;
        private SignInManager<APIUser> _signInManager;
        private RoleManager<APIUserRole> _roleManager;
        private readonly IEmployee _employee;

        public HomeController(IConfiguration config, UserManager<APIUser> userManager, APIIdentityContext identity, SignInManager<APIUser> signInManager, IEmployee employee, RoleManager<APIUserRole> roleManager = null)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _identity = identity;
            _roleManager = roleManager;
            _employee = employee;

        }

        [HttpGet("Welcome")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Welcome()
        {
            return Ok($"Welcome {GetHttpClaims()[1]}, your role is: {GetHttpClaims()[2]}");
        }

        [HttpGet("Profile")]
        public IActionResult GetProfileData()
        {
            return Ok(_employee.GetProfileData(GetUser()));
        }

        private List<string> GetHttpClaims()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var userClaims = claimsIdentity.Claims;

            return new List<string>()
            {
                userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value
            };

        }
        private APIUser GetUser()
        {
            return _identity.Users.Find(Convert.ToInt32(GetHttpClaims()[0]));
        }









    }
}
