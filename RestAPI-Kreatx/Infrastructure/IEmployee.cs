using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Models;
using System.Collections.Generic;

namespace RestAPI_Kreatx.Infrastructure
{
    public interface IEmployee
    {
        IActionResult UpdateProfilePicture([FromBody] ProfilePicture profilePicture, APIUser user);

        IActionResult UpdateProfileData([FromBody] EmployeeProfile profileData, APIUser user);

        List<EmployeeProfile> GetProfileData([FromBody] APIUser user);

        void CreateTask([FromBody] Tasks task);

        IActionResult UpdateTask([FromBody] Tasks task);

        void AssignTaskTo([FromBody] Tasks task, [FromBody] APIUser user);

        void MarkTaskAsFinished([FromBody] Tasks task);

        void ViewTask([FromBody] Tasks task);
    }
}
