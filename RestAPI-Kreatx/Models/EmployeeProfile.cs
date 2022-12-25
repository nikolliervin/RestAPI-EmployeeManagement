using System.ComponentModel.DataAnnotations;

namespace RestAPI_Kreatx.Models
{
    public class EmployeeProfile
    {
        [Required(ErrorMessage = "FirstName cannot be null")]
        [StringLength(24)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName cannot be null")]
        [StringLength(24)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }




    }
}
