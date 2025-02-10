using DataAccess.Data;
using WebApp.Models;
using WebApp.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context) => _context = context;

    public IActionResult Index()
    {
        var movies = _context.Movies.ToList();
        var movieViewModels = movies.Select(movie => new MovieViewModel
        {
            MovieId = movie.Id,
            Name = movie.Name,
            PosterUrl = movie.PosterUrl,
            Genres = GetGenresByMovieId(movie.Id)
        }).ToList();

        return View(movieViewModels);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private List<string?> GetGenresByMovieId(int movieId)
    {
        var genres = _context.MovieGenres
        .Where(mg => mg.MovieId == movieId)
            .Join(
                _context.Genres,
                mg => mg.GenreId,
                g => g.Id,
                (mg, g) => g.Name
            )
            .ToList();

        return genres;
    }
}
