using DataAccess.Data;
using WebApp.Models;
using WebApp.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using BusinessLogic.Interfaces;
using System.Security.Claims;
namespace WebApp.Controllers;

[Authorize]
public class TicketController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    private readonly ITicketService _ticketService;
    private readonly ISessionService _sessionService;

    public TicketController(AppDbContext context,
        UserManager<IdentityUser> userManager,
        ITicketService ticketService,
        ISessionService sessionService)
    {
        _context = context;
        _userManager = userManager;
        _ticketService = ticketService;
        _sessionService = sessionService;
    }

    public IActionResult ChooseSession(int movieId)
    {
        var sessions = _sessionService.GetActiveSessionsByMovie(movieId);
        ViewBag.MovieId = movieId;
        return View(sessions);
    }

    public IActionResult ChooseSeat(int sessionId)
    {
        var availableSeats = _ticketService.GetAvailablePlacementsForSession(sessionId);
        ViewBag.SessionId = sessionId;
        return View(availableSeats);
    }

    [HttpPost]
    public IActionResult BuySeat(int sessionId, int placeId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var ticket = _ticketService.BuyTicket(sessionId, placeId, userId);

        return RedirectToAction("PurchaseSuccess", new { ticketId = ticket.TicketId });
    }

    public IActionResult PurchaseSuccess(int ticketId)
    {
        var ticket = _ticketService.GetTicketById(ticketId);
        if (ticket == null)
        {
            return NotFound();
        }

        return View(ticket);
    }

    public async Task<IActionResult> MyTickets()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var tickets = _context.Tickets.Where(t => t.UserId == user.Id).ToList();
        if(tickets == null)
        {
            return NotFound($"Unable to load tickets for user with ID '{_userManager.GetUserId(User)}'.");
        }
        var ticketSession = tickets.Select(t => t.SessionId).ToList();
        var ticketPlacement = tickets.Select(t => t.PlaceId).ToList();

        var sessions = _context.Sessions.Where(s => ticketSession.Contains(s.Id)).ToList();
        var placements = _context.Placements.Where(p => ticketPlacement.Contains(p.Id)).ToList();


        var moviesSession = sessions.Select(s => s.MovieId).ToList();
        var movies = _context.Movies.Where(m => moviesSession.Contains(m.Id)).ToList();
        

        var model = new TicketSessionMovie()
        {
            Tickets = tickets,
            Placements = placements,
            Sessions = sessions,
            Movies = movies
        }; 
        return View(model);
    }
}