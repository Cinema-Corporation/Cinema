using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using DataAccess.Entities;

namespace DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                modelBuilder.Entity<User>().HasKey(entity => entity.UserId);
            });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var rootPath = Directory.GetParent(Environment.CurrentDirectory)?.FullName;
            var filePath = Path.Combine(rootPath!, "Infrastructure", "Data", "configDb.json");
            var json = File.ReadAllText(filePath);
            var configDb = JsonConvert.DeserializeObject<ConfigDb>(json) ?? throw new InvalidDataException("ConfigDb deserialization failed.");

            var serverVersion = new MySqlServerVersion(new Version(8,0,36));
            optionsBuilder.UseMySql(configDb.ConnectionString, serverVersion);
        }
    }
}