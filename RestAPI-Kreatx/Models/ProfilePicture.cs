using System.ComponentModel.DataAnnotations;

namespace RestAPI_Kreatx.Models
{
    public class ProfilePicture
    {
        [Required(ErrorMessage = "Picture name cannot be null")]
        [FileExtensions(Extensions = "jpg,jpeg,png")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Picture path cannot be null")]

        public string FileUrl { get; set; }
    }
}
