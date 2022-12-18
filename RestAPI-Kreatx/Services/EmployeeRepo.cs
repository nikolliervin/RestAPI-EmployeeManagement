using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Data;
using RestAPI_Kreatx.Infrastructure;
using RestAPI_Kreatx.Models;


namespace RestAPI_Kreatx.Services
{
    public class EmployeeRepo : IEmployee
    {
        private APIDbContext _db;
        private APIIdentityContext _identity;
        public EmployeeRepo(APIDbContext db, APIIdentityContext identity)
        {
            _db = db;
            _identity = identity;
        }

        void IEmployee.AssignTaskTo(Tasks task, APIUser user)
        {
            throw new System.NotImplementedException();
        }

        void IEmployee.CreateTask(Tasks task)
        {
            throw new System.NotImplementedException();
        }

        APIUser IEmployee.GetProfileData(APIUser user)
        {
            return _identity.Users.Find(user.Id);

        }

        void IEmployee.MarkTaskAsFinished(Tasks task)
        {
            throw new System.NotImplementedException();
        }

        IActionResult IEmployee.UpdateProfilePicture([FromBody] ProfilePicture profilePicture, [FromBody] APIUser user)
        {
            var curretUser = _identity.Users.Find(user.Id);

            if (profilePicture == null)
                return new StatusCodeResult(404);
            else
            {
                curretUser.UserProfilePicture = profilePicture.Name;
                curretUser.ProfilePictureUrl = profilePicture.FileUrl;
                _identity.SaveChanges();
            }
            return new JsonResult(200, "Ok!");

        }



        void IEmployee.ViewTask(Tasks task)
        {
            throw new System.NotImplementedException();
        }

        IActionResult IEmployee.UpdateTask(Tasks task)
        {
            throw new System.NotImplementedException();
        }
    }
}
