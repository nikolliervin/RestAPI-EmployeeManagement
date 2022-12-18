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

        void IEmployee.UpdateProfilePicture(string picture)
        {
            throw new System.NotImplementedException();
        }

        void IEmployee.UpdateTask(Tasks task)
        {
            throw new System.NotImplementedException();
        }

        void IEmployee.ViewTask(Tasks task)
        {
            throw new System.NotImplementedException();
        }
    }
}
