using DataAccess.Data;
using BusinessLogic.DTOs;
using DataAccess.Tmdb;
using DataAccess.Repositories;
namespace BusinessLogic.Services;

public class AdminService
{
    private readonly AppDbContext _context;
    private readonly TmdbRepository _tmdbRepository;

    public AdminService(AppDbContext context, TmdbRepository tmdbRepository)
    {
        _context = context;
        _tmdbRepository = tmdbRepository;
    }

    public void DeleteMovie(int movieId)
    {
        var movie = _context.Movies.Find(movieId);
        if (movie == null)
        {
            throw new Exception("Movie not found");
        }

        var movieGenres = _context.MovieGenres.Where(mg => mg.MovieId == movieId).ToList();

        foreach (var movieGenre in movieGenres)
        {
            _context.MovieGenres.Remove(movieGenre);
        }

        _context.Movies.Remove(movie);
        _context.SaveChanges();
    }
    public void AddMovie(MovieAndGenres movieAndGenres)
    {
        if (movieAndGenres.Movie.Name == null || movieAndGenres.Movie.PosterUrl == null || movieAndGenres.Movie.Description == null 
            || movieAndGenres.Movie.Duration == 0 || movieAndGenres.Movie.TrailerUrl == null || movieAndGenres.Movie.Rating < 0)
        {
            throw new ArgumentException("Invalid movie data.");
        }

        _context.Movies.Add(movieAndGenres.Movie);
        _context.SaveChanges();

        if (movieAndGenres.MovieGenres.Count == 0)
        {
            throw new ArgumentException("No genres provided for the movie.");
        }

        var uniqueGenres = movieAndGenres.MovieGenres.GroupBy(g => g.GenreId)
            .Select(g => g.First())
            .ToList();

        var movieId = movieAndGenres.Movie.Id;
        foreach (var genre in uniqueGenres)
        {
            genre.MovieId = movieId;
        }

        _context.AddRange(uniqueGenres);
        _context.SaveChanges();
    }
    public async Task AddSearchMovie(MovieSearchItem movie)
    {
        var newMovie = new DataAccess.Entities.Movie
        {
            Id = movie.Id,
            Name = movie.Title,
            Description = movie.Description,
            Rating = movie.Rating,
            Duration = movie.Duration,
            PosterUrl = movie.PosterPath,
            TrailerUrl = movie.TrailerUrl,
            Released = movie.Status == "Released"
        };

        if (newMovie.Name == null || newMovie.PosterUrl == null || newMovie.Description == null || newMovie.Duration == 0 || newMovie.TrailerUrl == null || newMovie.Rating < 0)
        {
            throw new ArgumentException("Invalid movie data.");
        }

        _context.Movies.Add(newMovie);
        _context.SaveChanges();

        await _tmdbRepository.SaveMovieGenresToDatabaseAsync(newMovie);
    }
}