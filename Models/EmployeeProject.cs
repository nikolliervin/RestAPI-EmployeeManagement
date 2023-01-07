using System.ComponentModel.DataAnnotations;

namespace RestAPI_EmployeeManagement.Models
{

    public class EmployeeProject
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User Id cannot be null")]
        public int UserId { get; set; }


        [Required(ErrorMessage = "User Id cannot be null")]
        public int ProjectId { get; set; }

    }
}
