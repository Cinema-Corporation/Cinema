using Newtonsoft.Json;

namespace DataAccess.Entities
{
    public class MovieSearchItem
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("poster_path")]
        public string? PosterPath { get; set; }
    }
}
