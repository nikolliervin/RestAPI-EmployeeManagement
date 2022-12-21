using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Data;
using RestAPI_Kreatx.Infrastructure;
using RestAPI_Kreatx.Models;
using System.Threading.Tasks;

namespace RestAPI_Kreatx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private APIIdentityContext _identity;
        private IEmployee _employee;
        private UserManager<APIUser> _mng;
        private RoleManager<APIUserRole> _role;


        public EmployeeController(APIIdentityContext identity, IEmployee employee, UserManager<APIUser> userManager, RoleManager<APIUserRole> rolemanager = null)
        {
            _identity = identity;
            _employee = employee;
            _mng = userManager;
            _role = rolemanager;

        }

        [HttpPost("CreateTask")]
        public IActionResult CreateTask([FromBody] Tasks task)
        {

            if (ModelState.IsValid)
                return Ok(_employee.CreateTask(task));
            else
                return BadRequest();


        }

        [HttpPost("MarkTaskTo")]
        public async Task<IActionResult> AssignTaskTo([FromBody] AssignTask assignTask)
        {
            if (ModelState.IsValid)
                return Ok(await _employee.AssignTaskTo(assignTask));
            else
                return BadRequest();
        }

        [HttpPost("FinishTask")]
        public IActionResult FinishTask([FromBody] string taskName)
        {
            if (taskName == null)
                return BadRequest();
            else
                return Ok(_employee.MarkTaskAsFinished(taskName));
        }

        [HttpGet("WatchAllTasks")]

        public IActionResult WatchAllTasks()
        {
            return Ok();
        }

    }
}
