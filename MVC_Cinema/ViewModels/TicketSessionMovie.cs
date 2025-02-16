using DataAccess.Entities;
namespace WebApp.ViewModels;

public class TicketSessionMovie()
{
    public required List<Movie> Movies { get; set; }
    public required List<Session> Sessions { get; set; }
    public required List<Placement> Placements { get; set; }
    public required List<Ticket> Tickets { get; set; }
}