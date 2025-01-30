using Newtonsoft.Json;

namespace DataAccess.Tmdb
{
    public class MovieSearchItem
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("poster_path")]
        public string? PosterPath { get; set; }
    }
}
