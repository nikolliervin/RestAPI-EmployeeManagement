using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Data;
using RestAPI_Kreatx.Infrastructure;
using RestAPI_Kreatx.Models;
using System.Collections.Generic;
using System.Linq;




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

        List<EmployeeProfile> IEmployee.GetProfileData(APIUser user)
        {

            var employeeData = (from u in _identity.Users
                                where u.Id == user.Id
                                select new EmployeeProfile
                                {
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    ProfilePicture = u.UserProfilePicture,
                                    PhoneNumber = u.PhoneNumber

                                }).ToList();

            return employeeData;

        }

        void IEmployee.MarkTaskAsFinished(Tasks task)
        {
            throw new System.NotImplementedException();
        }

        IActionResult IEmployee.UpdateProfilePicture([FromBody] ProfilePicture profilePicture, [FromBody] APIUser user)
        {

            user.UserProfilePicture = profilePicture.Name;
            user.ProfilePictureUrl = profilePicture.FileUrl;
            _identity.SaveChanges();

            return new JsonResult(200, "Profile picture Updated");

        }

        IActionResult IEmployee.UpdateProfileData([FromBody] EmployeeProfile profileData, APIUser user)
        {

            user.FirstName = profileData.FirstName;
            user.LastName = profileData.LastName;
            user.UserProfilePicture = profileData.ProfilePicture;
            user.PhoneNumber = profileData.PhoneNumber;
            _identity.SaveChanges();

            return new JsonResult(200, "User profile data updated");
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
