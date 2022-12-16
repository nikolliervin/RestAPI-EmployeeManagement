using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestAPI_Kreatx.Data;
using RestAPI_Kreatx.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI_Kreatx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private UserManager<APIUser> _userManager;
        private SignInManager<APIUser> _signInManager;
        private readonly APIIdentityContext _identity;
        private RoleManager<APIUserRole> _roleManager;

        public LoginController(IConfiguration config, SignInManager<APIUser> signInManager, UserManager<APIUser> userManager, RoleManager<APIUserRole> roleManager = null)
        {
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }



        //public async Task<IActionResult> register()
        //{
        //    var user = new APIUser
        //    {
        //        UserName = "user01",
        //        Email = "user01@api.com",

        //    };

        //    await _userManager.CreateAsync(user, "Apiuser1.!");
        //    var Employee = new APIUserRole
        //    {
        //        Name = "Employee",
        //        NormalizedName = "EMPLOYEE"
        //    };
        //    await _roleManager.CreateAsync(Employee);
        //    await _userManager.AddToRoleAsync(user, "Employee");
        //    return Ok();
        //}
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {

            var userFound = await _userManager.FindByNameAsync(user.username);
            if (userFound == null)
            {
                return StatusCode(404, "The user was not found");

            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(userFound, user.password, false, false);
                if (result.Succeeded)
                    return Ok(GenerateToken(userFound));
                else
                    return StatusCode(400, "Passowrd or username was not correct");
            }

        }

        private async Task<string> GenerateToken(APIUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userRole = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,userRole.ToString())
            };

            var token = new JwtSecurityToken(_config["JWT:Issuer"],
                _config["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
