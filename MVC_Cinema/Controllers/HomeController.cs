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

    public IActionResult Index(string filter)
    {
        var movies = _context.Movies.ToList();

        if (filter == "released")
        {
            movies = movies.Where(m => m.Released == true).ToList();
            ViewBag.Filter = "released";
        }
        else if (filter == "comingsoon")
        {
            movies = movies.Where(m => m.Released == false).ToList();
            ViewBag.Filter = "comingsoon";
        }
        else
        {
            ViewBag.Filter = "released";
        }

        var model = movies.Select(m => new MovieViewModel
        {
            MovieId = m.Id,
            Name = m.Name,
            PosterUrl = m.PosterUrl,
            Genres = GetGenresByMovieId(m.Id),
            Released = m.Released
        }).ToList();

        return View(model);
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
