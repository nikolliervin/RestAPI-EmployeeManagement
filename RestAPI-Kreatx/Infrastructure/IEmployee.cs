using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Models;
using RestAPI_Kreatx.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPI_Kreatx.Infrastructure
{
    public interface IEmployee
    {
        ActionResult<ProfilePicture> UpdateProfilePicture([FromBody] ProfilePicture profilePicture);

        ActionResult<EmployeeProfile> UpdateProfileData([FromBody] EmployeeProfile profileData);

        ActionResult<EmployeeProfileViewModel> GetProfileData();

        ActionResult<Tasks> CreateTask([FromBody] Tasks task, int projectId);

        ActionResult<Tasks> UpdateTask(Tasks task, int taskId);

        Task<AssignTask> AssignTaskTo(int taskId, APIUser user);

        ActionResult<Tasks> MarkTaskAsFinished(int taskId);

        List<TaskProjectViewModel> ViewTask();

    }
}
