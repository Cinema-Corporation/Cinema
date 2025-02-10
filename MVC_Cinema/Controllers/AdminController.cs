using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Data;
using WebApp.ViewModels;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Tmdb;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace WebApp.Controllers;

public class AdminController : Controller
{
    private readonly AppDbContext _context;
    private readonly TmdbRepository _tmdbRepository;

    public AdminController(AppDbContext context, TmdbRepository tmdbRepository)
    {
        _context = context;
        _tmdbRepository = tmdbRepository;
    }

    public IActionResult Movies()
    {
        var movies = _context.Movies.ToList();
        return View(movies);
    }
    public IActionResult SelectMovie(int MovieId)
    {
        var movie = _context.Movies.Find(MovieId);
        return View("EditMovie", movie);
    }
    public IActionResult EditMovie(Movie movie)
    {
        _context.Movies.Update(movie);
        _context.SaveChanges();
        return RedirectToAction("Movies");
    }
    public IActionResult DeleteMovie(int MovieId)
    {
        var movie = _context.Movies.Find(MovieId);
        if(movie == null)
        {
            return NotFound();
        }
        _context.Movies.Remove(movie);
        _context.SaveChanges();
        return RedirectToAction("Movies");
    }
    public IActionResult Movie()
    {
        return View("AddMovie");
    }

    public IActionResult Search()
    {
        return View("SearchMovie");
    }

    public async Task<IActionResult> SearchMovie(string? query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return View(new List<MovieSearchItem>());
        }

        var movies = await _tmdbRepository.SearchMoviesAsync(query);
        return View(movies);
    }

    [HttpGet("SearchMovieDetails/{id}")]
    public async Task<IActionResult> SearchMovieDetails(int id)
    {
        var movieDetails = await _tmdbRepository.GetMovieDetailsAsync(id);
        if (movieDetails == null)
        {
            return NotFound();
        }

        return View(movieDetails);
    }
    public IActionResult AddMovie(Movie movie)
    {
        if(movie.Name == null || movie.PosterUrl == null || movie.Description == null || movie.Duration == 0  || movie.TrailerUrl == null || movie.Rating == 0) 
        {
            return View("Error", new ErrorViewModel { RequestId = "Invalid movie data." });
        }
        _context.Movies.Add(movie);
        _context.SaveChanges();
        return RedirectToAction("Movies");
    }

    public IActionResult Sessions()
    {
        var MovieSessions = new SessionMovieViewModel
        {
            Movies = [.. _context.Movies],
            Sessions = [.. _context.Sessions]
        };
        return View(MovieSessions);
    }
    public IActionResult EditSession(int SessionId)
    {
        var session = _context.Sessions.Find(SessionId);
        return View(session);
    }
    public IActionResult EditSession(Session session)
    {
        _context.Sessions.Update(session);
        _context.SaveChanges();
        return RedirectToAction("Sessions");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
