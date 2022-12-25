using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private APIIdentityContext _identity;
        private readonly IEmployee _employee;

        public HomeController(APIIdentityContext identity, IEmployee employee)
        {
            _identity = identity;
            _employee = employee;
        }

        [HttpGet("Welcome")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult Welcome()
        {
            return Ok($"Welcome {GetHttpClaims()[1]}, your role is: {GetHttpClaims()[2]}");
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
        [HttpGet]
        public APIUser GetUser()
        {
            return _identity.Users.Find(Convert.ToInt32(GetHttpClaims()[0]));
        }









    }
}
