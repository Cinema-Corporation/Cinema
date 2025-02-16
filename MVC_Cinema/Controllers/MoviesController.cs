﻿using DataAccess.Data;
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
            MovieId = movie.Id,
            Name = movie.Name,
            Description = movie.Description?.Length > 0 ? movie.Description : "No description available.",
            PosterUrl = movie.PosterUrl
        }).ToList();
        
        return View(movieViewModels);
    }
    
    public IActionResult Details(int id)
    {
        var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
        if (movie == null)
        {
            return NotFound();
        }

        var movieViewModel = new MovieViewModel
        {
            MovieId = movie.Id,
            Name = movie.Name,
            Description = movie.Description,
            Rating = movie.Rating,
            Duration = movie.Duration,
            PosterUrl = movie.PosterUrl,
            TrailerUrl = movie.TrailerUrl,
            Genres = GetGenresByMovieId(movie.Id)
        };

        return View(movieViewModel);
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
