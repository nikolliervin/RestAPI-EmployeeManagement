using System.ComponentModel.DataAnnotations;

namespace RestAPI_EmployeeManagement.Models
{
    public class UpdateUser
    {
        [Required(ErrorMessage = "Username cannot be null!")]
        [StringLength(25)]
        public string Username { get; set; }


        [Required(ErrorMessage = "Email cannot be null!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(35)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Phone cannot be null!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Not a valid phone number")]
        [StringLength(15)]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "FirstName cannot be null!")]
        [StringLength(25)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName cannot be null!")]
        [StringLength(25)]
        public string LastName { get; set; }


        [FileExtensions(Extensions = "jpg,jpeg,png")]
        [Required(ErrorMessage = "ProfilePicture cannot be null!")]
        public string ProfilePicture { get; set; }


        [Required(ErrorMessage = "Picture url cannot be null!")]
        public string ProfilePictureUrl { get; set; }
    }
}
