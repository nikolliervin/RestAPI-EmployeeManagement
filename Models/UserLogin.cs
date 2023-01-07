using System.ComponentModel.DataAnnotations;

namespace RestAPI_EmployeeManagement.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "The username is required")]

        public string username { get; set; }

        [Required(ErrorMessage = "The password is required")]

        public string password { get; set; }
    }
}
