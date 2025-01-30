using DataAccess.Tmdb;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DataAccess.Repositories
{
    public class TmdbRepository
    {
        private readonly string? _apiKey;

        public TmdbRepository(IConfiguration configuration)
        {
            _apiKey = configuration["apiKey"];
        }

        public async Task<List<MovieSearchItem>> GetLatestMoviesAsync()
        {
            var url = $"https://api.themoviedb.org/3/movie/now_playing?api_key={_apiKey}&language=uk&page=1";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Не вдалося отримати список фільмів: {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var searchResult = JsonConvert.DeserializeObject<MovieSearchResult>(json);

            return searchResult.Results.Select(movie => new MovieSearchItem
            {
                Title = movie.Title,
                PosterPath = movie.PosterPath
            }).ToList();
        }
    }
}
