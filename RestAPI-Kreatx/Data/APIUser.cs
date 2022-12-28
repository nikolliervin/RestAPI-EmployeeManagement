﻿using Microsoft.AspNetCore.Identity;
using RestAPI_Kreatx;


namespace RestAPI_Kreatx.Models
{
    public class APIUser : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string UserProfilePicture { get; set; }

        public string ProfilePictureUrl { get; set; }




    }
}