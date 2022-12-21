using Microsoft.EntityFrameworkCore;

namespace RestAPI_Kreatx.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {

        }


    }
}
