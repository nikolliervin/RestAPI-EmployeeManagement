using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Data;
using RestAPI_Kreatx.Infrastructure;
using RestAPI_Kreatx.Models;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestAPI_Kreatx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Employee")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
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

        /// <summary>
        /// Gets the profile data for the currently logged in employee.
        /// </summary>
        [HttpGet("Profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProfileData()
        {
            return Ok(_employee.GetProfileData().Value);
        }



        /// <summary>
        /// Updates the profile picture for the currently logged in employee
        /// </summary>
        /// <param name="profilePicture">The profile picture object to be updated</param>
        [HttpPost("UpdateProfilePicture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProfilePicture([FromBody] ProfilePicture profilePicture)
        {
            if (ModelState.IsValid)
                return Ok(_employee.UpdateProfilePicture(profilePicture).Value);
            else
                return BadRequest();
        }


        /// <summary>
        /// Updates the profile data for the employee
        /// </summary>
        /// <param name="profileData">The new profile data object</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("UpdateProfileData")]
        public IActionResult UpdateProfileData([FromBody] EmployeeProfile profileData)
        {
            if (ModelState.IsValid)
                return Ok(_employee.UpdateProfileData(profileData).Value);
            else
                return BadRequest();
        }


        /// <summary>
        /// Creates a task for a certain project the employee is part of
        /// </summary>
        /// <param name="task">The task object to be created</param>
        /// <param name="project">The project where the task belongs</param>
        [HttpPost("CreateTask/{project}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateTask([FromBody] Tasks task, string project)
        {
            var projectId = _identity.Projects.Where(p => p.Name == project).Select(p => p.Id);
            var userProjectEntry = _identity.EmployeeProject.Where(p => p.ProjectId == projectId.FirstOrDefault() && p.UserId == GetUserId());

            if (ModelState.IsValid && projectId.ToList().Count != 0 && userProjectEntry.ToList().Count != 0)
                return Ok(_employee.CreateTask(task, projectId.FirstOrDefault()).Value);
            else
                return BadRequest($"You're not part of the project {project}");

        }


        /// <summary>
        /// Assigns a task that the employee has to a different employee
        /// </summary>
        /// <param name="taskUser">The model to assign the task to an employee</param>
        [HttpPost("AssignTaskTo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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


        /// <summary>
        /// Marks a certain task that the employee has as completed
        /// </summary>
        /// <param name="taskName">Name of the task</param>
        [HttpPut("FinishTask/{taskName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult FinishTask(string taskName)
        {
            var task = _identity.Tasks.Where(t => t.TaskName == taskName && t.UserId == GetUserId()).Select(t => t.Id);

            if (task.ToList().Count != 0)
                return Ok(_employee.MarkTaskAsFinished(task.FirstOrDefault()).Value);
            else
                return NotFound($"Task {taskName} was not found in your account!");


        }


        /// <summary>
        /// Gets all the tasks from the project the employee is part of
        /// </summary>
        [HttpGet("WatchAllTasks")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult WatchAllTasks()
        {
            if (_identity.Tasks.Where(t => t.UserId == GetUserId()).Select(t => t.ProjectId).ToList().Count == 0)
                return NotFound("Youre not part of any project");

            return Ok(_employee.ViewTask());
        }


        /// <summary>
        /// Updates the tasks that user selects
        /// </summary>
        /// <param name="task">The new task object to be updated</param>
        /// <param name="taskName">Name of the old task</param>
        [HttpPut("UpdateTask/{taskName}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

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
