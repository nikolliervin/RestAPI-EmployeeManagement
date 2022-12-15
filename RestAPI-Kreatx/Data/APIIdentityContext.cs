using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestAPI_Kreatx.Models;
using System.ComponentModel.DataAnnotations;

namespace RestAPI_Kreatx.Data
{
    public class APIIdentityContext : IdentityDbContext<APIUser, APIUserRole, int>
    {

        public APIIdentityContext(DbContextOptions<APIIdentityContext> options) : base(options)
        {

        }

        [Required(ErrorMessage = "A first name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "A last name is required")]
        public string LastName { get; set; }



    }
}
