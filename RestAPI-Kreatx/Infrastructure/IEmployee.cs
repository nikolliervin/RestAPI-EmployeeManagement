using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Models;
using RestAPI_Kreatx.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPI_Kreatx.Infrastructure
{
    public interface IEmployee
    {
        IActionResult UpdateProfilePicture([FromBody] ProfilePicture profilePicture, APIUser user);

        IActionResult UpdateProfileData([FromBody] EmployeeProfile profileData, APIUser user);

        List<EmployeeProfile> GetProfileData([FromBody] APIUser user);

        IActionResult CreateTask([FromBody] Tasks task);

        IActionResult UpdateTask([FromBody] Tasks task);

        Task<IActionResult> AssignTaskTo([FromBody] AssignTask assignTask);

        IActionResult MarkTaskAsFinished([FromBody] string taskName);

        List<TaskProjectViewModel> ViewTask();

    }
}
