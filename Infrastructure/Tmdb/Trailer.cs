using Newtonsoft.Json;

namespace DataAccess.Tmdb
{
    public class Trailer
    {
        [JsonProperty("key")]
        public string? Key { get; set; }
    }
}
