using DataAccess.Data;
using DataAccess.Entities;
using WebApp.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace WebApp.Controllers;

public class AdminController : Controller
{
    private readonly AppDbContext _context;

    public AdminController(AppDbContext context)
    {
        _context = context;
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

    public IActionResult AddMovie(Movie movie)
    {
        if (movie.Name == null || movie.PosterUrl == null || movie.Description == null || movie.Duration == 0 || movie.TrailerUrl == null || movie.Rating == 0) 
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

    public IActionResult Session(int SessionId)
    {
        var movies = _context.Movies.ToList();
        var session = _context.Sessions.Find(SessionId);
        var movieSessions = new EditSessionViewModel(movies, session);
        return View("EditSession",movieSessions);
    }

    public IActionResult EditSession(Session session)
    {
        _context.Sessions.Update(session);
        _context.SaveChanges();
        return RedirectToAction("Sessions");
    }

    public IActionResult DeleteSession(int SessionId)
    {
        var session = _context.Sessions.Find(SessionId);
        if(session == null)
        {
            return NotFound();
        }
        _context.Sessions.Remove(session);
        _context.SaveChanges();
        return RedirectToAction("Sessions");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
