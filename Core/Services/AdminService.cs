using DataAccess.Data;
using BusinessLogic.DTOs;
using DataAccess.Tmdb;
using DataAccess.Repositories;
using DataAccess.Interfaces;
using DataAccess.Entities;
using BusinessLogic.Interfaces;
namespace BusinessLogic.Services;

public class AdminService : IAdminService
{
    private readonly IRepository<Movie> _movieRepository;
    private readonly IRepository<MovieGenre> _movieGenreRepository;
    private readonly TmdbRepository _tmdbRepository;

    public AdminService(IRepository<Movie> movieRepository, IRepository<MovieGenre> movieGenreRepository, TmdbRepository tmdbRepository)
    {
        _movieRepository = movieRepository;
        _movieGenreRepository = movieGenreRepository;
        _tmdbRepository = tmdbRepository;
    }

    public void DeleteMovie(int movieId)
    {
        var movie = _movieRepository
            .GetAll()
            .FirstOrDefault(m => m.Id == movieId);
        if (movie == null)
        {
            throw new Exception("Movie not found");
        }

        var movieGenres = _movieGenreRepository.GetAll().Where(mg => mg.MovieId == movieId).ToList();

        foreach (var movieGenre in movieGenres)
        {
            _movieGenreRepository.Delete(movieGenre);
        }

        _movieRepository.Delete(movie);
        _movieRepository.Save();
        _movieGenreRepository.Save();
    }
    public void AddMovie(MovieAndGenres movieAndGenres)
    {
        if (movieAndGenres.Movie.Name == null || movieAndGenres.Movie.PosterUrl == null || movieAndGenres.Movie.Description == null 
            || movieAndGenres.Movie.Duration == 0 || movieAndGenres.Movie.TrailerUrl == null || movieAndGenres.Movie.Rating < 0)
        {
            throw new ArgumentException("Invalid movie data.");
        }

        _movieRepository.Insert(movieAndGenres.Movie);
        _movieRepository.Save();

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
            _movieGenreRepository.Insert(genre);
        }
        _movieGenreRepository.Save();
    }
    public async Task AddSearchMovie(MovieSearchItem movie)
    {
        var newMovie = new Movie
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

        _movieRepository.Insert(newMovie);
        _movieRepository.Save();

        await _tmdbRepository.SaveMovieGenresToDatabaseAsync(newMovie);
    }
}