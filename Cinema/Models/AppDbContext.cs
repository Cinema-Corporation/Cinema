using Microsoft.EntityFrameworkCore;
using Cinema.Models;

namespace Cinema.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                modelBuilder.Entity<User>().HasKey(entity => entity.IdUser);
            });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=localhost;user=root;password=Password;database=Cinema";

            var serverVersion = new MySqlServerVersion(new Version(8,0,36));
            optionsBuilder.UseMySql(connectionString, serverVersion);
            
        }
    }
}