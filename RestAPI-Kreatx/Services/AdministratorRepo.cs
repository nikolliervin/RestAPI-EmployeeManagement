using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI_Kreatx.Data;
using RestAPI_Kreatx.Infrastructure;
using RestAPI_Kreatx.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI_Kreatx.Services
{
    public class AdministratorRepo : IAdministrator
    {
        APIIdentityContext _identity;
        UserManager<APIUser> _userManager;
        public AdministratorRepo(APIIdentityContext identity, UserManager<APIUser> userManager)
        {
            _identity = identity;
            _userManager = userManager;
        }

        async Task<APIUser> IAdministrator.CreateUser([FromBody] UserLogin user)
        {
            var newUser = new APIUser { UserName = user.username, Email = $"{user.username}@api.com" };
            var result = await _userManager.CreateAsync(newUser, user.password);
            await _userManager.AddToRoleAsync(newUser, "Employee");
            return newUser;
        }

        ActionResult<APIUser> IAdministrator.RemoveUser(string username)
        {
            var user = _identity.Users.Where(u => u.UserName == username).FirstOrDefault();
            var deletedUser = user.UserName;
            _identity.Remove(user);
            _identity.SaveChanges();
            return user;
        }

        ActionResult<UpdateUser> IAdministrator.UpdateUser(string username, [FromBody] UpdateUser user)
        {
            var userFound = _identity.Users.Where(u => u.UserName == username).FirstOrDefault();

            userFound.UserName = user.Username;
            userFound.Email = user.Email;
            userFound.PhoneNumber = user.PhoneNumber;
            userFound.FirstName = user.FirstName;
            userFound.LastName = user.LastName;
            userFound.UserProfilePicture = user.ProfilePicture;
            userFound.ProfilePictureUrl = user.ProfilePictureUrl;

            _identity.SaveChanges();

            return user;

        }

        ActionResult<Tasks> IAdministrator.CreateTask([FromBody] Tasks task, string user, string project)
        {
            var userId = _identity.Users.Where(u => u.UserName == user).Select(u => u.Id).FirstOrDefault();
            var projectId = _identity.Projects.Where(p => p.Name == project).Select(p => p.Id).FirstOrDefault();

            var newTask = new Tasks
            {
                TaskName = task.TaskName,
                TaskDesc = task.TaskDesc,
                IsFinished = task.IsFinished,
                ProjectId = projectId,
                UserId = userId
            };

            _identity.Add(newTask);
            _identity.SaveChanges();

            return newTask;
        }

        ActionResult<Tasks> IAdministrator.UpdateTask([FromBody] UpdateTask task)
        {
            var oldTask = _identity.Tasks.Where(t => t.TaskName == t.TaskName).FirstOrDefault();

            oldTask.TaskName = task.NewTaskName;
            oldTask.TaskDesc = task.NewTaskDesc;
            oldTask.IsFinished = task.IsFinished;

            _identity.Update(oldTask);
            _identity.SaveChanges();

            return oldTask;
        }

        ActionResult<Tasks> IAdministrator.DeleteTask(string taskName)
        {
            var task = _identity.Tasks.Where(t => t.TaskName == taskName).FirstOrDefault();
            _identity.Remove(task);
            _identity.SaveChanges();

            return task;
        }

        ActionResult<EmployeeProject> IAdministrator.AddToProject(IQueryable<int> employee, IQueryable<int> project)
        {
            var query = (from p in _identity.EmployeeProject
                         where
                        p.UserId == employee.ToList()[0] &&
                        p.ProjectId == project.ToList()[0]
                         select new EmployeeProject
                         {
                             ProjectId = p.ProjectId,
                             UserId = p.UserId

                         }).ToList();

            if (query.Count == 0)
            {
                var empProj = new EmployeeProject
                {
                    ProjectId = project.ToList()[0],
                    UserId = employee.ToList()[0]
                };

                _identity.EmployeeProject.Add(empProj);
                _identity.SaveChanges();
                return empProj;

            }
            else
                return query[0];

        }

        ActionResult<EmployeeProject> IAdministrator.RemoveFromProject(int employeeId, int projectId)
        {
            var record = _identity.EmployeeProject.Where(p => p.UserId == employeeId && p.ProjectId == projectId).FirstOrDefault();

            _identity.Remove(record);
            _identity.SaveChanges();
            return record;
        }

        ActionResult<Tasks> IAdministrator.AssignTask(string task, string user)
        {
            var taskObj = _identity.Tasks.Where(t => t.TaskName == task).FirstOrDefault();
            var userObj = _identity.Users.Where(u => u.UserName == user).FirstOrDefault();

            taskObj.UserId = userObj.Id;
            _identity.SaveChanges();

            return taskObj;
        }

        ActionResult<Tasks> IAdministrator.CompleteTask(string task)
        {
            var taskObj = _identity.Tasks.Where(t => t.TaskName == task).FirstOrDefault();

            taskObj.IsFinished = true;
            _identity.SaveChanges();

            return taskObj;
        }

        ActionResult<Tasks> IAdministrator.RemoveTask(string task)
        {
            var taskObj = _identity.Tasks.Where(t => t.TaskName == task).FirstOrDefault();
            _identity.Remove(taskObj);
            _identity.SaveChanges();

            return taskObj;
        }

        ActionResult<Projects> IAdministrator.AddProject(Projects project)
        {
            _identity.Projects.Add(project);
            _identity.SaveChanges();

            return project;
        }

        ActionResult<Projects> IAdministrator.UpdateProject(string projectName, Projects project)
        {
            var oldProject = _identity.Projects.Where(p => p.Name == projectName).FirstOrDefault();

            var proj = new Projects
            {
                Id = oldProject.Id,
                Name = project.Name,
                ProjectDesc = project.ProjectDesc
            };

            _identity.Update(project);
            _identity.SaveChanges();

            return proj;


        }

        ActionResult<Projects> IAdministrator.RemoveProject(int projectId)
        {
            var projectEntry = _identity.Projects.Find(projectId);

            _identity.Projects.Remove(projectEntry);
            _identity.SaveChanges();

            return projectEntry;
        }
    }
}
