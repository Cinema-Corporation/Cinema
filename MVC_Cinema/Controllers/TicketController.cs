using DataAccess.Data;
using WebApp.Models;
using WebApp.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
namespace WebApp.Controllers;

public class TicketController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public TicketController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<IActionResult> Index()
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