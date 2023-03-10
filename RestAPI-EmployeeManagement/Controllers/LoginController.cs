using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestAPI_EmployeeManagement.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI_EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]

    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private UserManager<APIUser> _userManager;
        private SignInManager<APIUser> _signInManager;

        public LoginController(IConfiguration config, SignInManager<APIUser> signInManager, UserManager<APIUser> userManager, RoleManager<APIUserRole> roleManager = null)
        {
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        /// <summary>
        /// Logs the users in and generates the token
        /// </summary>
        /// <param name="user">Employee username and password</param>
        /// <returns></returns>
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
                {
                    var token = GenerateToken(userFound).Result;
                    return Ok(token);
                }
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
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,userRole[0].ToString())
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
