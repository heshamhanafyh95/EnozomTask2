using Microsoft.EntityFrameworkCore;
using EnozomTask.Models;

namespace EnozomTask.Data
{
    public class AppDbContext:DbContext
    {
        /* public string ConnectionString { get; set; }

         public AppDbContext(string connectionString)
         {
             this.ConnectionString = connectionString;
         }

         private MySqlConnection GetConnection()
         {
             return new MySqlConnection(ConnectionString);
         }*/

        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = Configuration.GetConnectionString("DemoDb");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Holiday> Holidays { get; set; } = null!;
    }
}
