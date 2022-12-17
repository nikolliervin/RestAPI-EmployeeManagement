using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestAPI_Kreatx.Data;
using RestAPI_Kreatx.Models;
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

        public HomeController(IConfiguration config, UserManager<APIUser> userManager, APIIdentityContext identity, SignInManager<APIUser> signInManager, RoleManager<APIUserRole> roleManager = null)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _identity = identity;
            _roleManager = roleManager;

        }

        [HttpGet("Welcome")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Welcome()
        {
            return Ok($"Welcome {GetHttpClaims()[0]}, your role is: {GetHttpClaims()[1]}");
        }

        private List<string> GetHttpClaims()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var userClaims = claimsIdentity.Claims;

            return new List<string>()
            {
                userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value
            };

        }









    }
}
