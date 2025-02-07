using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Tmdb;
using Newtonsoft.Json;
namespace DataAccess.Repositories;

public class TmdbRepository : ITmdb
{
    private readonly string? _apiKey;
    private readonly AppDbContext _context;
    public TmdbRepository(string apiKey, AppDbContext context)
    {
        _apiKey = apiKey;
        _context = context;
    }

    public async Task<List<MovieSearchItem>> GetLatestMoviesAsync()
    {
        var url = $"https://api.themoviedb.org/3/movie/now_playing?api_key={_apiKey}&page=1";

        using var client = new HttpClient();
        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Не вдалося отримати список фільмів: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();
        var searchResult = JsonConvert.DeserializeObject<MovieSearchResult>(json);

        var movieItems = new List<MovieSearchItem>();

        var topMovies = searchResult?.Results.Take(10);

        foreach (var movie in searchResult.Results)
        {
            var trailerUrlKey = await GetMovieTrailerKeyAsync(movie.Id);
            var runtime = await GetMovieRuntimeAsync(movie.Id);

            movieItems.Add(new MovieSearchItem
            {
                Id = movie.Id,
                Title = movie.Title,
                PosterPath = movie.PosterPath,
                Description = movie.Description,
                Rating = movie.Rating,
                Duration = runtime,
                TrailerUrl = trailerUrlKey 
            });
        }

        return movieItems;
    }


    public async Task SaveLatestMoviesToDatabaseAsync()
    {
        var movies = await GetLatestMoviesAsync();

        var existingMovieTitles = _context.Movies
            .Select(m => m.Name)
            .ToHashSet();

        var newMovies = movies
            .Where(m => !existingMovieTitles.Contains(m.Title))
            .Select(m => new Movie
            {
                Id = m.Id,
                Name = m.Title,
                Description = m.Description,
                Rating = m.Rating,
                Duration = m.Duration,
                PosterUrl = m.PosterPath,
                TrailerUrl = m.TrailerUrl
            })
            .ToList();

        if (newMovies.Any())
        {
            await _context.Movies.AddRangeAsync(newMovies);
            await _context.SaveChangesAsync();
        }
    }

    private async Task<string?> GetMovieTrailerKeyAsync(int movieId)
    {
        var url = $"https://api.themoviedb.org/3/movie/{movieId}/videos?api_key={_apiKey}";

        using var client = new HttpClient();
        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Не вдалося отримати трейлер для фільму з ID {movieId}: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();
        var videoResult = JsonConvert.DeserializeObject<MovieTrailerResult>(json);

        return videoResult?.Results?.FirstOrDefault()?.Key;
    }

    private async Task<int> GetMovieRuntimeAsync(int movieId)
    {
        var url = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={_apiKey}";

        using var client = new HttpClient();
        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return 0;
        }

        var json = await response.Content.ReadAsStringAsync();
        var movieDetails = JsonConvert.DeserializeObject<MovieRunTime>(json);

        return movieDetails?.Runtime ?? 0;
    }
}
