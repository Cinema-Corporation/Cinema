using BusinessLogic.DTOs;
namespace BusinessLogic.Interfaces;

public interface ITicketService
{
    IEnumerable<PlacementDTO> GetAvailablePlacementsForSession(int sessionId);
    TicketDTO BuyTicket(int sessionId, int placeId, string userId);
    TicketDTO? GetTicketById(int ticketId);
}
