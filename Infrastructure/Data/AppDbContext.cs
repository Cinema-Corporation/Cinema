using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using DataAccess.Entities;
using DataAccess.Entities.Configurations;
namespace DataAccess.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Hall> Halls { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Placement> Placements { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketDetails> TicketDetails { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new HallConfiguration());
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new PlacementConfiguration());
        modelBuilder.ApplyConfiguration(new SessionConfiguration());
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyConfiguration(new TicketDetailsConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var rootPath = Directory.GetParent(Environment.CurrentDirectory)?.FullName;
        var filePath = Path.Combine(rootPath!, "Infrastructure", "Data", "config.json");
        var json = File.ReadAllText(filePath);
        var config = JsonConvert.DeserializeObject<ConfigStructure>(json) ?? throw new InvalidDataException("Config deserialization failed.");

        var serverVersion = new MySqlServerVersion(new Version(8,0,36));
        optionsBuilder.UseMySql(config.ConnectionString, serverVersion);
    }
}
