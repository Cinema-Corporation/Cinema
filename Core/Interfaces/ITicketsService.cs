using BusinessLogic.DTOs;
using DataAccess.Entities;
namespace BusinessLogic.Interfaces;

public interface ITicketsService
{
    IEnumerable<Ticket> GetAvailiblePlacements(int sessionId);
    TicketDTO BuyTicket(int sessionId, Ticket ticket);
}
