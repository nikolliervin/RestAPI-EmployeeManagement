using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Infrastructure;
using RestAPI_Kreatx.Models;
using System.Threading.Tasks;

namespace RestAPI_Kreatx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private IEmployee _employee;

        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
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
            return Ok(_employee.ViewTask());
        }

        [HttpPost("UpdateTask")]

        public IActionResult UpdateTask([FromBody] UpdateTask task)
        {
            if (ModelState.IsValid)
                return Ok(_employee.UpdateTask(task));
            else
                return BadRequest();
        }

    }
}
