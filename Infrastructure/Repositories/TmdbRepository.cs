﻿using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Tmdb;
using Newtonsoft.Json;

namespace DataAccess.Repositories
{
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

        public async Task<List<TMDbGenre>> GetAllGenresAsync()
        {
            var url = $"https://api.themoviedb.org/3/genre/movie/list?api_key={_apiKey}";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Не вдалося отримати список жанрів: {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var genreResult = JsonConvert.DeserializeObject<GenreResult>(json);

            return genreResult?.Genres ?? new List<TMDbGenre>();
        }

        public async Task SaveGenresToDatabaseAsync()
        {
            var genres = await GetAllGenresAsync();

            var existingGenreIds = _context.Genres
                .Select(g => g.Id)
                .ToHashSet();

            var newGenres = genres
                .Where(g => !existingGenreIds.Contains(g.Id))
                .Select(g => new Genre
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToList();

            if (newGenres.Any())
            {
                await _context.Genres.AddRangeAsync(newGenres);
                await _context.SaveChangesAsync();
            }
        }


        public async Task SaveLatestMoviesToDatabaseAsync()
        {
            var movies = await GetLatestMoviesAsync();

            var existingMovieIds = _context.Movies.Select(m => m.Id).ToHashSet();
            var newMovies = new List<Movie>();

            foreach (var movie in movies)
            {
                if (!existingMovieIds.Contains(movie.Id))
                {
                    newMovies.Add(new Movie
                    {
                        Id = movie.Id,
                        Name = movie.Title,
                        Description = movie.Description,
                        Rating = movie.Rating,
                        Duration = movie.Duration,
                        PosterUrl = movie.PosterPath,
                        TrailerUrl = movie.TrailerUrl
                    });
                }
            }

            if (newMovies.Any())
            {
                await _context.Movies.AddRangeAsync(newMovies);
                await _context.SaveChangesAsync();
            }

            await SaveMovieGenresToDatabaseAsync(newMovies);
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

        private async Task<List<int>> GetMovieGenresAsync(int movieId)
        {
            var url = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={_apiKey}&language=uk";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new List<int>(); 
            }

            var json = await response.Content.ReadAsStringAsync();
            var movieDetails = JsonConvert.DeserializeObject<GenreResult>(json);

            return movieDetails?.Genres?.Select(g => g.Id).ToList() ?? new List<int>();
        }

        public async Task SaveMovieGenresToDatabaseAsync(List<Movie> movies)
        {
            var allGenres = _context.Genres.ToList();
            var existingMovieGenres = _context.MovieGenres
                .Select(mg => new { mg.MovieId, mg.GenreId })
                .ToHashSet();

            var movieGenresToAdd = new List<MovieGenre>();

            foreach (var movie in movies)
            {
                var genreIds = await GetMovieGenresAsync(movie.Id);

                foreach (var genreId in genreIds)
                {
                    if (allGenres.Any(g => g.Id == genreId) &&
                        !existingMovieGenres.Contains(new { MovieId = movie.Id, GenreId = genreId }))
                    {
                        movieGenresToAdd.Add(new MovieGenre
                        {
                            MovieId = movie.Id,
                            GenreId = genreId
                        });
                    }
                }
            }

            if (movieGenresToAdd.Any())
            {
                await _context.MovieGenres.AddRangeAsync(movieGenresToAdd);
                await _context.SaveChangesAsync();
            }
        }
    }


}
