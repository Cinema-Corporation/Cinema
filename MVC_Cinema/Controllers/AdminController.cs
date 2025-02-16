using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Tmdb;
using BusinessLogic.Services;
using BusinessLogic.DTOs;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using WebApp.ViewModels;
using System.Diagnostics;
namespace WebApp.Controllers;

public class AdminController : Controller
{
    private readonly AppDbContext _context;
    private readonly TmdbRepository _tmdbRepository;
    private readonly AdminService _adminService;
    

    public AdminController(AppDbContext context, TmdbRepository tmdbRepository, AdminService adminService)
    {
        _context = context;
        _tmdbRepository = tmdbRepository;
        _adminService = adminService;
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
        _adminService.DeleteMovie(MovieId);
        return RedirectToAction("Movies");
    }

    public IActionResult Movie()
    {
        var MovieAndGenres = new MovieAndGenres
        {
            Movie = new Movie(),
            Genres = _context.Genres.ToList(),
            MovieGenres = new List<MovieGenre>()
        };
        return View("AddMovie", MovieAndGenres);
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
    public IActionResult AddMovie(MovieAndGenres movieAndGenres)
    {
        _adminService.AddMovie(movieAndGenres);
        return RedirectToAction("Movies");
    }

    public async Task<IActionResult> AddSearchMovie(MovieSearchItem movie)
    {
        await _adminService.AddSearchMovie(movie);
        return RedirectToAction("Movies");
    }

    public IActionResult Sessions()
    {
        var MovieSessions = new SessionsMoviesViewModel
        {
            Movies = [.. _context.Movies],
            Sessions = [.. _context.Sessions]
        };
        return View(MovieSessions);
    }
    public IActionResult SelectSession(int id)
    {
        var session = _context.Sessions.Find(id);
        
        if (session == null)
            return NotFound();
        
        var sessionMovies = new SessionMoviesViewModel
        {
            Movies = _context.Movies.ToList(),
            Session = session
        };
        return View( "EditSession" ,sessionMovies);
    }

    public IActionResult EditSession(Session session)
    {
        session.TimeEnd = session.TimeStart.AddMinutes(_context.Movies.Find(session.MovieId).Duration);
        _context.Sessions.Update(session);
        _context.SaveChanges();
        return RedirectToAction("Sessions");
    }
    public IActionResult DeleteSession(int id)
    {
        var session = _context.Sessions.Find(id);
        if(session == null)
        {
            return NotFound();
        }
        _context.Sessions.Remove(session);
        _context.SaveChanges();
        return RedirectToAction("Sessions");
    }
    public IActionResult AddSession(Session session)
    {
        if(session.MovieId == 0 || session.TimeStart == DateTime.MinValue)
            return View("Error", new ErrorViewModel { RequestId = "Invalid session data." });
        
        session.TimeEnd = session.TimeStart.AddMinutes(_context.Movies.Find(session.MovieId).Duration);

        if(session.TimeEnd < DateTime.Now)
            return View("Error", new ErrorViewModel { RequestId = "Session time is in the past." });
        
        _context.Sessions.Add(session);
        _context.SaveChanges();
        return RedirectToAction("Sessions");
    }
    public IActionResult Session()
    {
        return View("AddSession", new SessionMoviesViewModel { Movies = _context.Movies.ToList(), Session = new Session() });
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
