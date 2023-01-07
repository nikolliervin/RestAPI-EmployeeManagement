using Microsoft.AspNetCore.Identity;
using RestAPI_EmployeeManagement;


namespace RestAPI_EmployeeManagement.Models
{
    public class APIUser : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string UserProfilePicture { get; set; }

        public string ProfilePictureUrl { get; set; }




    }
}
