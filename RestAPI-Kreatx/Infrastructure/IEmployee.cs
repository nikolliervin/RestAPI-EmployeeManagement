using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPI_Kreatx.Infrastructure
{
    public interface IEmployee
    {
        IActionResult UpdateProfilePicture([FromBody] ProfilePicture profilePicture);

        IActionResult UpdateProfileData([FromBody] EmployeeProfile profileData);

        List<EmployeeProfile> GetProfileData();

        IActionResult CreateTask([FromBody] Tasks task);

        IActionResult UpdateTask([FromBody] UpdateTask task);

        Task<IActionResult> AssignTaskTo([FromBody] AssignTask assignTask);

        IActionResult MarkTaskAsFinished([FromBody] string taskName);

        IActionResult ViewTask();

    }
}
