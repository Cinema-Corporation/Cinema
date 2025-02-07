using DataAccess.Data;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
namespace WebApp.Controllers;

public class MoviesController : Controller
{
    private readonly AppDbContext _context;

    public MoviesController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var movies = _context.Movies.ToList();
        var movieViewModels = movies.Select(movie => new MovieViewModel
        {
            Title = movie.Name,
            Description = movie.Description?.Length > 0 ? movie.Description : "No description available.",
            PosterUrl = movie.PosterUrl
        }).ToList();
        
        return View(movieViewModels);
    }
}
