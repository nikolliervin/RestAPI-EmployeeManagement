using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Data;
using RestAPI_Kreatx.Infrastructure;
using RestAPI_Kreatx.Models;
using RestAPI_Kreatx.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestAPI_Kreatx.Services
{
    public class EmployeeRepo : IEmployee
    {
        private APIIdentityContext _identity;
        private UserManager<APIUser> _userManager;
        private readonly IHttpContextAccessor _httpAccessor;
        public EmployeeRepo(APIIdentityContext identity, UserManager<APIUser> userManager, IHttpContextAccessor accesor)
        {
            _identity = identity;
            _userManager = userManager;
            _httpAccessor = accesor;
        }

        async Task<IActionResult> IEmployee.AssignTaskTo([FromBody] AssignTask assignTask)
        {
            var result = await _userManager.FindByNameAsync(assignTask.Username);


            if (result == null || await _userManager.IsInRoleAsync(result, "Admin"))
                return new JsonResult(404, "Employee was not found");
            else
            {

                var record = _identity.Tasks.Where(t => t.TaskName == assignTask.TaskName).FirstOrDefault();
                record.UserId = result.Id;
                _identity.Update(record);
                _identity.SaveChanges();
            }
            return new JsonResult(200, $"Task assigned to: {result.UserName}");

        }

        IActionResult IEmployee.CreateTask(Tasks task)
        { /*DOTO userid=getuserid*/
            var userProjectId = _identity.Projects.Where(p => 1 == GetUserId()).Select(p => p.Id).FirstOrDefault();
            var newTask = new Tasks
            {
                TaskName = task.TaskName,
                TaskDesc = task.TaskDesc,
                IsFinished = task.IsFinished,
                ProjectId = userProjectId,
                UserId = GetUserId(),
            };

            _identity.Tasks.Add(newTask);
            _identity.SaveChanges();

            return new JsonResult(200, $"Task {task.TaskName} was created successfully!");
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

        IActionResult IEmployee.MarkTaskAsFinished(string taskName)
        {
            var taskObj = _identity.Tasks.Where(u => u.TaskName == taskName && u.UserId == GetUserId()).FirstOrDefault();

            if (taskObj == null)
                return new JsonResult(404, $"Task {taskName} was not found");
            else
            {
                var record = _identity.Tasks.Where(t => t.TaskName == taskName && t.UserId == GetUserId()).FirstOrDefault();
                record.IsFinished = true;
                _identity.Update(record);
                _identity.SaveChanges();
            }

            return new JsonResult(200, $"Task {taskName} is marked as Finished");
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



        List<TaskProjectViewModel> IEmployee.ViewTask()
        {
            return new List<TaskProjectViewModel>();
        }

        IActionResult IEmployee.UpdateTask(Tasks task)
        {
            throw new System.NotImplementedException();
        }

        int GetUserId()
        {
            return int.Parse(_httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }


    }
}
