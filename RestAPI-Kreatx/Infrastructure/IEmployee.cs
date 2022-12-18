using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Models;

namespace RestAPI_Kreatx.Infrastructure
{
    public interface IEmployee
    {
        void UpdateProfilePicture([FromBody] string picture);

        APIUser GetProfileData([FromBody] APIUser user);

        void CreateTask([FromBody] Tasks task);

        void UpdateTask([FromBody] Tasks task);

        void AssignTaskTo([FromBody] Tasks task, [FromBody] APIUser user);

        void MarkTaskAsFinished([FromBody] Tasks task);

        void ViewTask([FromBody] Tasks task);
    }
}
