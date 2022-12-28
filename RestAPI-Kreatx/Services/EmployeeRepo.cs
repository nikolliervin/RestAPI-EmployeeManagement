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

        async Task<AssignTask> IEmployee.AssignTaskTo(int taskId, APIUser user)
        {
            var taskobj = _identity.Tasks.Find(taskId);

            taskobj.UserId = user.Id;
            _identity.SaveChanges();
            return new AssignTask { TaskName = taskobj.TaskName, Username = user.UserName };
        }

        ActionResult<Tasks> IEmployee.CreateTask([FromBody] Tasks task, int projectId)
        {

            var newTask = new Tasks
            {
                TaskName = task.TaskName,
                TaskDesc = task.TaskDesc,
                IsFinished = task.IsFinished,
                ProjectId = projectId,
                UserId = GetUserId(),
            };

            _identity.Tasks.Add(newTask);
            _identity.SaveChanges();

            return newTask;

        }
        //TODO: Refactor to get more fields from different tables
        ActionResult<EmployeeProfileViewModel> IEmployee.GetProfileData()
        {

            var employeeData = _identity.Users.Where(u => u.Id == GetUserId()).FirstOrDefault();
            var employeeTasks = (from t in _identity.Tasks where t.UserId == GetUserId() select t.TaskName);
            var employeeProjects = (from p in _identity.EmployeeProject
                                    where p.UserId == GetUserId()
                                    join
                                    n in _identity.Projects on p.ProjectId equals n.Id
                                    select n.Name);

            return new EmployeeProfileViewModel
            {
                FirstName = employeeData.FirstName,
                LastName = employeeData.LastName,
                Username = employeeData.UserName,
                PhoneNumber = employeeData.PhoneNumber,
                Tasks = employeeTasks.ToList(),
                Projects = employeeProjects.ToList()
            };

        }

        ActionResult<Tasks> IEmployee.MarkTaskAsFinished(int taskId)
        {
            var taskObj = _identity.Tasks.Find(taskId);
            taskObj.IsFinished = true;
            _identity.SaveChanges();

            return taskObj;
        }

        ActionResult<ProfilePicture> IEmployee.UpdateProfilePicture([FromBody] ProfilePicture profilePicture)
        {
            var user = _identity.Users.Find(GetUserId());
            user.UserProfilePicture = profilePicture.Name;
            user.ProfilePictureUrl = profilePicture.FileUrl;
            _identity.SaveChanges();

            return profilePicture;

        }

        ActionResult<EmployeeProfile> IEmployee.UpdateProfileData([FromBody] EmployeeProfile profileData)
        {
            var user = _identity.Users.Find(GetUserId());
            user.FirstName = profileData.FirstName;
            user.LastName = profileData.LastName;
            user.PhoneNumber = profileData.PhoneNumber;
            _identity.SaveChanges();

            return profileData;
        }



        List<TaskProjectViewModel> IEmployee.ViewTask()
        {

            var projects = from t in _identity.Tasks
                           where t.UserId == GetUserId()
                           select t.ProjectId;


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

            return query;

        }
        ActionResult<Tasks> IEmployee.UpdateTask([FromBody] Tasks task, int taskId)
        {
            var taskObj = _identity.Tasks.Where(t => t.Id == taskId).FirstOrDefault();

            taskObj.IsFinished = task.IsFinished;
            taskObj.TaskName = task.TaskName;
            taskObj.TaskDesc = task.TaskDesc;

            _identity.SaveChanges();
            return taskObj;

        }



        int GetUserId()
        {
            return int.Parse(_httpAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }




    }
}
