using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace WebApp.Controllers;

public class SessionsController : Controller
{
    private readonly ISessionService _sessionService;

    public SessionsController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public IActionResult Index()
    {
        var sessionsWithMovies = _sessionService.GetActiveSessionsWithMovies();
        return View(sessionsWithMovies);
    }
}
