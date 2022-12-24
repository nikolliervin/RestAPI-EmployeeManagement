using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Data;
using RestAPI_Kreatx.Infrastructure;
using RestAPI_Kreatx.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI_Kreatx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserLogin user)
        {
            if (await _userManager.FindByNameAsync(user.username) != null)
                return BadRequest($"User {user.username} alredy exists!");
            else
                return Ok(await _admin.CreateUser(user));

        }

        [HttpPost("DeleteUser")]

        public IActionResult DeleteUser([FromBody] string username)
        {
            if (username == null || _identity.Users.Where(u => u.UserName == username).ToList().Count == 0)
                return BadRequest($"User {username} does not exist");
            else
                return Ok($"User {_admin.RemoveUser(username).Value} was removed successfully");

        }

        [HttpPost("UpdateUser")]

        public IActionResult UpdateUser(string username, [FromBody] UpdateUser user)
        {
            if (_identity.Users.Where(u => u.UserName == username).ToList().Count == 0)
                return NotFound($"User {username} was not found!");

            return Ok(_admin.UpdateUser(username, user).Value);
        }

        [HttpPost("CreateTask")]

        public IActionResult CreateTask([FromBody] Tasks task, string user, string project)
        {
            if (_identity.Users.Where(u => u.UserName == user).ToList().Count == 0 ||
                _identity.Projects.Where(u => u.Name == project).ToList().Count == 0)
                return NotFound($"User {user} or project {project} does not exist");

            return Ok(_admin.CreateTask(task, user, project).Value);
        }

        [HttpPost("UpdateTask")]
        public IActionResult UpdateTask([FromBody] UpdateTask task)
        {
            if (_identity.Tasks.Where(t => t.TaskName == task.TaskName).ToList().Count == 0)
                return NotFound($"Task {task.TaskName} was not found");
            return Ok(_admin.UpdateTask(task).Value);
        }

        [HttpPost("DeleteTask")]

        public IActionResult DeleteTask(string taskName)
        {
            if (_identity.Tasks.Where(t => t.TaskName == taskName).ToList().Count == 0)
                return NotFound($"Task {taskName} was not found");

            return Ok(_admin.DeleteTask(taskName).Value);
        }

        [HttpPost("AddToProject")]

        public IActionResult AddToProject(string employee, string project)
        {
            var employeeEntry = _identity.Users.Where(u => u.UserName == employee).Select(e => e.Id);
            var projectEntry = _identity.Projects.Where(p => p.Name == project).Select(p => p.Id);

            if (employeeEntry.ToList().Count == 0 || projectEntry.ToList().Count == 0)
                return NotFound($"Employee {employee} or project {project} was not found");

            return Ok(_admin.AddToProject(employeeEntry, projectEntry).Value);

        }

        [HttpPost("RemoveEmployeeFrom")]

        public IActionResult RemoveEmployeeFrom(string employee, string project)
        {
            var employeeId = _identity.Users.Where(u => u.UserName == employee).Select(u => u.Id).FirstOrDefault();
            var projectId = _identity.Projects.Where(u => u.Name == project).Select(u => u.Id).FirstOrDefault();

            var employeeExists = _identity.EmployeeProject.Where(u => u.UserId == employeeId && u.ProjectId == projectId).ToList();

            if (employeeExists.Count == 0)
                return NotFound($"Employee {employee} is not part of project {project}");

            return Ok(_admin.RemoveFromProject(employeeId, projectId));
        }

        [HttpPost("AssignTask")]

        public IActionResult AssignTask(string task, string user)
        {
            if (_identity.Users.Where(u => u.UserName == user).ToList().Count == 0 ||
                _identity.Tasks.Where(u => u.TaskName == task).ToList().Count == 0)
                return NotFound($"User {user} or task {task} was not found");

            return Ok();

        }

        [HttpPost("CompleteTask")]

        public IActionResult CompleteTask(string task)
        {
            if (_identity.Tasks.Where(t => t.TaskName == task).ToList().Count == 0)
                return NotFound($"Task {task} was not found");

            return Ok(_admin.CompleteTask(task));
        }

        [HttpPost("RemoveTask")]

        public IActionResult RemoveTask(string task)
        {
            if (_identity.Tasks.Where(t => t.TaskName == task).ToList().Count == 0)
                return NotFound($"Task {task} does not exist");

            return Ok(_admin.RemoveTask(task));
        }




    }
}
