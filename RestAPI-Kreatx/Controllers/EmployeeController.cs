using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Data;
using RestAPI_Kreatx.Infrastructure;
using RestAPI_Kreatx.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestAPI_Kreatx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Employee")]
    public class EmployeeController : ControllerBase
    {

        private IEmployee _employee;
        private APIIdentityContext _identity;
        private readonly IHttpContextAccessor _httpAccessor;
        private UserManager<APIUser> _userManager;

        public EmployeeController(IEmployee employee, APIIdentityContext identity, IHttpContextAccessor accessor, UserManager<APIUser> userManager)
        {
            _employee = employee;
            _identity = identity;
            _httpAccessor = accessor;
            _userManager = userManager;

        }

        [HttpGet("Profile")]

        public IActionResult GetProfileData()
        {
            return Ok(_employee.GetProfileData().Value);
        }

        [HttpPost("UpdateProfilePicture")]
        public IActionResult UpdateProfilePicture([FromBody] ProfilePicture profilePicture)
        {
            if (ModelState.IsValid)
                return Ok(_employee.UpdateProfilePicture(profilePicture));
            else
                return BadRequest();
        }

        [HttpPost("UpdateProfileData")]
        public IActionResult UpdateProfileData([FromBody] EmployeeProfile profileData)
        {
            if (ModelState.IsValid)
                return Ok(_employee.UpdateProfileData(profileData).Value);
            else
                return BadRequest();
        }

        [HttpPost("CreateTask")]
        public IActionResult CreateTask([FromBody] Tasks task, string project)
        {
            var projectId = _identity.Projects.Where(p => p.Name == project).Select(p => p.Id);
            var userProjectEntry = _identity.EmployeeProject.Where(p => p.ProjectId == projectId.FirstOrDefault() && p.UserId == GetUserId());

            if (ModelState.IsValid && projectId.ToList().Count != 0 && userProjectEntry.ToList().Count != 0)
                return Ok(_employee.CreateTask(task, projectId.FirstOrDefault()).Value);
            else
                return BadRequest($"You're not part of the project {project}");

        }

        [HttpPost("AssignTaskTo")]
        public async Task<ActionResult> AssignTaskTo([FromBody] AssignTask taskUser)
        {

            var task = _identity.Tasks.Where(t => t.TaskName == taskUser.TaskName);
            var user = _identity.Users.Where(u => u.UserName == taskUser.Username);
            if (task.ToList().Count > 0 && user.ToList().Count > 0)
            {
                var taskRecord = _identity.Tasks.Where(t => t.TaskName == taskUser.TaskName && t.UserId == GetUserId()).Select(t => t.Id);
                var userObj = await _userManager.FindByNameAsync(taskUser.Username);
                var isUserAdmin = await _userManager.IsInRoleAsync(userObj, "Admin");
                var userExists = _identity.Users.Where(u => u.UserName == taskUser.Username).ToList().Count > 0;

                if (isUserAdmin == false && userExists == true && userObj.UserName == taskUser.Username && taskRecord.ToList().Count != 0)
                    return Ok(await _employee.AssignTaskTo(taskRecord.FirstOrDefault(), userObj));
                else
                    return BadRequest();
            }

            return NotFound($"Task {taskUser.TaskName} or user {taskUser.Username} was not found!");


        }

        [HttpPut("FinishTask/{taskName}")]
        public IActionResult FinishTask(string taskName)
        {
            var task = _identity.Tasks.Where(t => t.TaskName == taskName && t.UserId == GetUserId()).Select(t => t.Id);

            if (task.ToList().Count != 0)
                return Ok(_employee.MarkTaskAsFinished(task.FirstOrDefault()).Value);
            else
                return NotFound($"Task {taskName} was not found!");


        }

        [HttpGet("WatchAllTasks")]

        public IActionResult WatchAllTasks()
        {
            if (_identity.Tasks.Where(t => t.UserId == GetUserId()).Select(t => t.ProjectId).ToList().Count == 0)
                return NotFound("Youre not part of any project");

            return Ok(_employee.ViewTask());
        }

        [HttpPut("UpdateTask/{taskName}")]

        public IActionResult UpdateTask([FromBody] Tasks task, string taskName)
        {
            var taskEntry = _identity.Tasks.Where(t => t.TaskName == taskName).Select(t => t.Id);
            if (taskEntry.ToList().Count == 0)
                return NotFound($"Task {taskName} was not found");
            return Ok(_employee.UpdateTask(task, taskEntry.FirstOrDefault()).Value);
        }

        int GetUserId()
        {
            return int.Parse(_httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

    }
}
