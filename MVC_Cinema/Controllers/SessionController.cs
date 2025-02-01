using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;

namespace WebApp.Controllers;

public class SessionController : Controller
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public IActionResult Index()
    {
        return View(_sessionService.GetAllSessions());
    }
}
