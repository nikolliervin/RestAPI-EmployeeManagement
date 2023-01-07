using System.ComponentModel.DataAnnotations;

namespace RestAPI_EmployeeManagement.Models
{
    public class AssignTask
    {
        [Required(ErrorMessage = "Username is required to assign task to another employee")]
        [StringLength(12)]
        public string Username { get; set; }

        [Required(ErrorMessage = "TaskName is required to assign task to another employee")]
        [StringLength(25)]
        public string TaskName { get; set; }
    }
}
