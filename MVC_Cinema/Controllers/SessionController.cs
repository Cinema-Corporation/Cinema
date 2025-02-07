using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace WebApp.Controllers;

public class SessionController : Controller
{
    private readonly ISessionService _sessionService;
    private readonly IMovieService _movieService;

    public SessionController(ISessionService sessionService, IMovieService movieService)
    {
        _sessionService = sessionService;
        _movieService = movieService;
    }

    public IActionResult Index()
    {
        var sessionsWithMovies = _sessionService.GetActiveSessionsWithMovies();
        return View(sessionsWithMovies);
    }
}
