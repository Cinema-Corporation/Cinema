using Newtonsoft.Json;

namespace DataAccess.Tmdb
{
    public class GenreResult
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("genres")]
        public List<TMDbGenre>? Genres { get; set; }
    }
}
