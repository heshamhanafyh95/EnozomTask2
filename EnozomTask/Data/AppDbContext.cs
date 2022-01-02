using Microsoft.EntityFrameworkCore;
using EnozomTask.Models;

namespace EnozomTask.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Holiday> Holidays { get; set; } = null!;
    }
}
