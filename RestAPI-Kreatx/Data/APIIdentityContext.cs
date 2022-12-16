using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestAPI_Kreatx.Models;

namespace RestAPI_Kreatx.Data
{
    public class APIIdentityContext : IdentityDbContext<APIUser, APIUserRole, int>
    {

        public APIIdentityContext(DbContextOptions<APIIdentityContext> options) : base(options)
        {

        }




    }
}
