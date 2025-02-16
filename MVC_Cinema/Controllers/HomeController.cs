using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Data;
using DataAccess.Repositories;
using WebApp.Models;
using WebApp.ViewModels;
namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;
    private readonly TmdbRepository _tmdbRepository;

    public HomeController(ILogger<HomeController> logger, AppDbContext context, TmdbRepository tmdbRepository)
    {
        _logger = logger;
        _context = context;
        _tmdbRepository = tmdbRepository;
    }

    public IActionResult Index()
    {
        var movies = _context.Movies.ToList();

        var movieViewModels = movies.Select(movie => new MovieViewModel
        {
            Title = movie.Name,
            PosterPath = movie.PosterUrl,
        }).ToList();

        return View(movieViewModels);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
