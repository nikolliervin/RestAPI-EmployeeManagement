using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Data;
using RestAPI_Kreatx.Infrastructure;
using RestAPI_Kreatx.Models;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RestAPI_Kreatx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class AdminController : Controller
    {
        IAdministrator _admin;
        UserManager<APIUser> _userManager;
        APIIdentityContext _identity;
        public AdminController(IAdministrator admin, UserManager<APIUser> userManager, APIIdentityContext identity)
        {
            _admin = admin;
            _userManager = userManager;
            _identity = identity;
        }


        /// <summary>
        /// Creates a new user of role "Employee"
        /// </summary>
        /// <param name="user">The user object to be created</param>
        [HttpPost("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] UserLogin user)
        {
            if (await _userManager.FindByNameAsync(user.username) != null)
                return BadRequest($"User {user.username} alredy exists!");
            else
                return Ok(await _admin.CreateUser(user));

        }


        /// <summary>
        /// Deletes the employee based on the username
        /// </summary>
        /// <param name="username">The username of the employee</param>
        [HttpDelete("DeleteUser/{username}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult DeleteUser(string username)
        {
            if (username == null || _identity.Users.Where(u => u.UserName == username).ToList().Count == 0)
                return BadRequest($"User {username} does not exist");
            else
                return Ok($"User {_admin.RemoveUser(username).Value} was removed successfully");

        }


        /// <summary>
        /// Updates the employee that the admin selects to the json object
        /// </summary>
        /// <param name="username">The employee selected</param>
        /// <param name="user">The new employee object</param>
        [HttpPut("UpdateUser/{username}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult UpdateUser(string username, [FromBody] UpdateUser user)
        {
            if (_identity.Users.Where(u => u.UserName == username).ToList().Count == 0)
                return NotFound($"User {username} was not found!");

            return Ok(_admin.UpdateUser(username, user).Value);
        }


        /// <summary>
        /// Creates a task for a certain project and assigns it to a user
        /// </summary>
        /// <param name="task">Task object[Name, Desc, Status]</param>
        /// <param name="user">The employee to assign the task to</param>
        /// <param name="project">The project where task belongs</param>
        [HttpPost("CreateTask/{user}/{project}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult CreateTask([FromBody] Tasks task, string user, string project)
        {
            if (_identity.Users.Where(u => u.UserName == user).ToList().Count == 0 ||
                _identity.Projects.Where(u => u.Name == project).ToList().Count == 0)
                return NotFound($"User {user} or project {project} does not exist");

            return Ok(_admin.CreateTask(task, user, project).Value);
        }


        /// <summary>
        /// Selects the task from the [TaskName] and updates it to a new one
        /// </summary>
        /// <param name="task">The model used to identify and update task</param>
        [HttpPost("UpdateTask")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateTask([FromBody] UpdateTask task)
        {
            if (_identity.Tasks.Where(t => t.TaskName == task.TaskName).ToList().Count == 0)
                return NotFound($"Task {task.TaskName} was not found");
            return Ok(_admin.UpdateTask(task).Value);
        }


        /// <summary>
        /// Deletes the task based on the TaskName the admin types
        /// </summary>
        /// <param name="taskName">The name of the task to be deleted</param>
        /// <returns></returns>
        [HttpDelete("DeleteTask/{taskName}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteTask(string taskName)
        {
            if (_identity.Tasks.Where(t => t.TaskName == taskName).ToList().Count == 0)
                return NotFound($"Task {taskName} was not found");

            return Ok(_admin.DeleteTask(taskName).Value);
        }


        /// <summary>
        /// Adds a certain employee to a project
        /// </summary>
        /// <param name="employee">Username of the employee</param>
        /// <param name="project">Name of the project</param>
        /// <returns></returns>
        [HttpPost("AddToProject/{employee}/{project}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult AddToProject(string employee, string project)
        {
            var employeeEntry = _identity.Users.Where(u => u.UserName == employee).Select(e => e.Id);
            var projectEntry = _identity.Projects.Where(p => p.Name == project).Select(p => p.Id);

            if (employeeEntry.ToList().Count == 0 || projectEntry.ToList().Count == 0)
                return NotFound($"Employee {employee} or project {project} was not found");

            return Ok(_admin.AddToProject(employeeEntry, projectEntry).Value);

        }


        /// <summary>
        /// Removes the selected employee from a certain project
        /// </summary>
        /// <param name="employee">The username of the employee</param>
        /// <param name="project">The Name of the project</param>
        [HttpDelete("RemoveEmployeeFrom/{employee}/{project}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult RemoveEmployeeFrom(string employee, string project)
        {
            var employeeId = _identity.Users.Where(u => u.UserName == employee).Select(u => u.Id).FirstOrDefault();
            var projectId = _identity.Projects.Where(u => u.Name == project).Select(u => u.Id).FirstOrDefault();

            var employeeExists = _identity.EmployeeProject.Where(u => u.UserId == employeeId && u.ProjectId == projectId).ToList();

            if (employeeExists.Count == 0)
                return NotFound($"Employee {employee} is not part of project {project}");

            return Ok(_admin.RemoveFromProject(employeeId, projectId).Value);
        }


        /// <summary>
        /// Assigns a task to a certain employee
        /// </summary>
        /// <param name="task">The name of the task</param>
        /// <param name="user">The username of the employee</param>
        /// <returns></returns>
        [HttpPost("AssignTask/{task}/{user}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult AssignTask(string task, string user)
        {
            if (_identity.Users.Where(u => u.UserName == user).ToList().Count == 0 ||
                _identity.Tasks.Where(u => u.TaskName == task).ToList().Count == 0)
                return NotFound($"User {user} or task {task} was not found");

            return Ok(_admin.AssignTask(task, user).Value);

        }


        /// <summary>
        /// Marks a task as completed
        /// </summary>
        /// <param name="task">Name of the task</param>
        [HttpPut("CompleteTask/{task}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult CompleteTask(string task)
        {
            if (_identity.Tasks.Where(t => t.TaskName == task).ToList().Count == 0)
                return NotFound($"Task {task} was not found");

            return Ok(_admin.CompleteTask(task).Value);
        }


        /// <summary>
        /// Removes a task
        /// </summary>
        /// <param name="task">The name of the task to be removed</param>
        [HttpDelete("RemoveTask/{task}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult RemoveTask(string task)
        {
            if (_identity.Tasks.Where(t => t.TaskName == task).ToList().Count == 0)
                return NotFound($"Task {task} does not exist");

            return Ok(_admin.RemoveTask(task).Value);
        }


        /// <summary>
        /// Adds a new project
        /// </summary>
        /// <param name="project">The project object to be added</param>
        [HttpPost("AddProject")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult AddProject([FromBody] Projects project)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(_admin.AddProject(project).Value);
        }


        /// <summary>
        /// Updates the selected project to the new one
        /// </summary>
        /// <param name="projectName">The name of the project selected</param>
        /// <param name="project">The new project object</param>
        [HttpPut("UpdateProject/{projectName}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult UpdateProject(string projectName, [FromBody] Projects project)
        {

            if (!ModelState.IsValid)
                return BadRequest();
            else if (_identity.Projects.Where(p => p.Name == projectName).ToList().Count == 0)
                return NotFound($"Project {projectName} was not found");
            else
                return Ok(_admin.UpdateProject(projectName, project).Value);

        }


        /// <summary>
        /// Removes the project selected
        /// </summary>
        /// <param name="projectName">Name of the project</param>
        [HttpDelete("RemoveProject/{projectName}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult RemoveProject(string projectName)
        {
            var projectObj = _identity.Projects.Where(p => p.Name == projectName);
            var projectId = projectObj.Select(p => p.Id).FirstOrDefault();

            if (projectObj.ToList().Count == 0)
                return NotFound($"Project {projectName} was not found");
            else if (_identity.Tasks.Where(t => t.ProjectId == projectId).ToList().Count >= 1)
                return BadRequest($"Project {projectName} cannot be removed because it has open tasks!");
            else
                return Ok(_admin.RemoveProject(projectId).Value);
        }




    }
}
