using Newtonsoft.Json;

namespace DataAccess.Tmdb
{
    public class MovieSearchResult
    {
        [JsonProperty("results")]
        public List<MovieSearchItem> Results { get; set; } = [];
    }
}
