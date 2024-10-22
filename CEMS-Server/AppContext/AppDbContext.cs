using Microsoft.EntityFrameworkCore;
using CEMS_Server.Models;

namespace CEMS_Server.AppContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<cems_position> cems_position { get; set; }

    }
}