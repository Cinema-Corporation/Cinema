using Microsoft.EntityFrameworkCore;
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
    public DbSet<Placement> Placements { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new HallConfiguration());
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new PlacementConfiguration());
        modelBuilder.ApplyConfiguration(new SessionConfiguration());
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
