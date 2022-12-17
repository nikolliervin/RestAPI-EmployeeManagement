using Microsoft.EntityFrameworkCore;
using RestAPI_Kreatx.Models;

namespace RestAPI_Kreatx.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {

        }

        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<Projects> Projects { get; set; }
    }
}
