using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI_Kreatx.Infrastructure
{
    public interface IAdministrator
    {
        Task<APIUser> CreateUser([FromBody] UserLogin user);

        ActionResult<UpdateUser> UpdateUser(string username, [FromBody] UpdateUser user);

        ActionResult<APIUser> RemoveUser(string username);

        ActionResult<Tasks> CreateTask(Tasks task, string user, string project);

        ActionResult<Tasks> UpdateTask(UpdateTask task);

        ActionResult<Tasks> DeleteTask(string taskName);

        ActionResult<EmployeeProject> AddToProject(IQueryable<int> employee, IQueryable<int> project);

        ActionResult<EmployeeProject> RemoveFromProject(int employeeId, int projectId);

        ActionResult<Tasks> AssignTask(string task, string user);

        ActionResult<Tasks> CompleteTask(string task);

        ActionResult<Tasks> RemoveTask(string task);

        ActionResult<Projects> AddProject(Projects project);
        ActionResult<Projects> UpdateProject(string projectName, Projects project);
        ActionResult<Projects> RemoveProject(int projectId);

    }
}
