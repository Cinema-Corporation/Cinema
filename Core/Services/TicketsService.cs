using BusinessLogic.DTOs;
using DataAccess.Entities;
using DataAccess.Interfaces;
using BusinessLogic.Interfaces;
using AutoMapper;
namespace BusinessLogic.Services;

public class TicketService : ITicketService
{
    private readonly IRepository<Ticket> _ticketRepository;
    private readonly IRepository<Session> _sessionRepository;
    private readonly IRepository<Placement> _placementRepository;
    private readonly IRepository<Hall> _hallRepository;
    private readonly IMapper _mapper;

    public TicketService(
        IRepository<Ticket> ticketRepository,
        IRepository<Session> sessionRepository,
        IRepository<Placement> placementRepository,
        IRepository<Hall> hallRepository,
        IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _sessionRepository = sessionRepository;
        _placementRepository = placementRepository;
        _hallRepository = hallRepository;
        _mapper = mapper;
    }

    public IEnumerable<PlacementDTO> GetAvailablePlacementsForSession(int sessionId)
    {
        var session = _sessionRepository.GetById(sessionId);
        if (session == null)
        {
            return Enumerable.Empty<PlacementDTO>();
        }

        var hall = _hallRepository.GetById(session.HallId);
        if (hall == null)
        {
            return Enumerable.Empty<PlacementDTO>();
        }

        var allPlacements = _placementRepository.GetAll()
            .Where(p => p.HallId == hall.Id)
            .ToList();

        var usedPlaces = _ticketRepository.GetAll()
            .Where(t => t.SessionId == session.Id)
            .Select(t => t.PlaceId)
            .ToHashSet();

        var availablePlacements = allPlacements
            .Where(p => !usedPlaces.Contains(p.Id))
            .ToList();

        return _mapper.Map<IEnumerable<PlacementDTO>>(availablePlacements);
    }

    public TicketDTO BuyTicket(int sessionId, int placeId, string userId)
    {
        var session = _sessionRepository.GetById(sessionId);
        if (session == null)
            throw new Exception("Session not found.");

        var placement = _placementRepository.GetById(placeId);
        if (placement == null)
            throw new Exception("Placement not found.");

        if (placement.HallId != session.HallId)
            throw new Exception("This seat does not belong to the hall of the session.");

        var existingTicket = _ticketRepository.GetAll()
            .FirstOrDefault(t => t.SessionId == sessionId && t.PlaceId == placeId);
        if (existingTicket != null)
            throw new Exception("This seat is already taken.");

        var ticket = new Ticket
        {
            SessionId = sessionId,
            PlaceId = placeId,
            UserId = userId,
            Price = placement.Luxe ? 230.00m : 130.00m,
            PaymentDate = DateTime.Now
        };

        _ticketRepository.Insert(ticket);
        _ticketRepository.Save();

        return _mapper.Map<TicketDTO>(ticket);
    }

    public TicketDTO? GetTicketById(int ticketId)
    {
        var ticket = _ticketRepository.GetById(ticketId);
        if (ticket == null) return null;
        return _mapper.Map<TicketDTO>(ticket);
    }
    
}
