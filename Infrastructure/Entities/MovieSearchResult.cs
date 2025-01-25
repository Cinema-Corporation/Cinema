using Newtonsoft.Json;

namespace DataAccess.Entities
{
    public class MovieSearchResult
    {
        [JsonProperty("results")]
        public List<MovieSearchItem> Results { get; set; }
    }
}
