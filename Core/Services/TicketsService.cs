using BusinessLogic.DTOs;
using DataAccess.Entities;
using DataAccess.Interfaces;
using BusinessLogic.Interfaces;
using AutoMapper;
namespace BusinessLogic.Services;

public class TicketsService : ITicketsService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Session> _ticketsRepository;

    public TicketsService(IMapper mapper, IRepository<Session> sessionRepository)
    {
        _mapper = mapper;
        _ticketsRepository = sessionRepository;
    }
    public IEnumerable<Ticket> GetAvailiblePlacements(int sessionId)
    {
        var ReservedSessionTickets = _ticketsRepository.
        GetAll()
        .Where(s => s.Id == sessionId)
        .ToList();
       return (IEnumerable<Ticket>)ReservedSessionTickets;
    }
    public TicketDTO BuyTicket(int sessionId, Ticket ticket)
    {
        var availableTickets = GetAvailiblePlacements(sessionId);
        if (!availableTickets.Any())
            throw new Exception("No available tickets for this session");
        
        else if(availableTickets.Contains(ticket))
            throw new Exception("This ticket is already reserved");
        
        else
        {
            return _mapper.Map<TicketDTO>(ticket);
        }
    }
}