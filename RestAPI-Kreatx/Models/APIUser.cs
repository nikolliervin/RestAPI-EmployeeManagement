using Microsoft.AspNetCore.Identity;

namespace RestAPI_Kreatx.Models
{
    public class APIUser : IdentityUser<int>
    {
        public string UserProfilePicture { get; set; }

        public string ProfilePictureUrl { get; set; }



    }
}
