using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Data;
using RestAPI_Kreatx.Infrastructure;
using RestAPI_Kreatx.Models;
using RestAPI_Kreatx.ViewModels;
using System;
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
                if (record == null)
                    return new JsonResult(404, $"The task: {assignTask.TaskName} does not exist");
                record.UserId = result.Id;
                _identity.Update(record);
                _identity.SaveChanges();
            }

            return new JsonResult(200, $"Task assigned to: {result.UserName}");

        }

        IActionResult IEmployee.CreateTask(Tasks task)
        {   /*TODO: Change Project ID 1.Put project as string parameter*/
            var userProjectId = 1/*_identity.Users.Where(p => p.Id == GetUserId()).Select(p => p.ProjectId==1).FirstOrDefault()*/;
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

        List<EmployeeProfile> IEmployee.GetProfileData()
        {

            var employeeData = (from u in _identity.Users
                                where u.Id == GetUserId()
                                select new EmployeeProfile
                                {
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
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

            return new JsonResult($"Task {taskName} is marked as Finished");
        }

        IActionResult IEmployee.UpdateProfilePicture([FromBody] ProfilePicture profilePicture)
        {
            var user = _identity.Users.Find(GetUserId());
            user.UserProfilePicture = profilePicture.Name;
            user.ProfilePictureUrl = profilePicture.FileUrl;
            _identity.SaveChanges();

            return new JsonResult("Profile picture Updated");

        }

        IActionResult IEmployee.UpdateProfileData([FromBody] EmployeeProfile profileData)
        {
            var user = _identity.Users.Find(GetUserId());
            user.FirstName = profileData.FirstName;
            user.LastName = profileData.LastName;
            user.PhoneNumber = profileData.PhoneNumber;
            _identity.SaveChanges();

            return new JsonResult("Profile data updated");
        }



        IActionResult IEmployee.ViewTask()
        {

            var projects = from t in _identity.Tasks
                           where t.UserId == GetUserId()
                           select t.ProjectId;

            if (projects == null)
                return new JsonResult(404, "You are not part of any project");

            var query = (from t in _identity.Tasks
                         join p in _identity.Projects
                         on t.ProjectId equals p.Id
                         join u in _identity.Users
                         on t.UserId equals u.Id
                         where projects.Contains(p.Id)
                         select new TaskProjectViewModel
                         {
                             Username = u.UserName,
                             ProjectName = p.Name,
                             TaskName = t.TaskName,
                             TaskDesc = t.TaskDesc,
                             IsFinished = t.IsFinished
                         }).ToList();

            if (query.Count == 0)
                return new JsonResult(404, "The project youre part of has no tasks created");

            return new OkObjectResult(query);

        }
        IActionResult IEmployee.UpdateTask(UpdateTask task)
        {
            var taskObj = _identity.Tasks.Where(t => t.TaskName == task.TaskName).FirstOrDefault();
            if (taskObj == null)
                return new JsonResult(404, $"Task {task.TaskName} was not found");
            else
            {
                taskObj.TaskName = task.NewTaskName;
                taskObj.TaskDesc = task.NewTaskDesc;
                taskObj.IsFinished = task.IsFinished;
                _identity.Update(taskObj);
                _identity.SaveChanges();

            }
            return new JsonResult(200, $"Task {task.TaskName} was updated successfully!");
        }

        int GetUserId()
        {
            return int.Parse(_httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }


    }
}
