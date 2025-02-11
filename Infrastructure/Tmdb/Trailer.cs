using Newtonsoft.Json;

namespace DataAccess.Tmdb
{
    public class Trailer
    {
        [JsonProperty("key")]
        public string? Key { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; } 

        [JsonProperty("site")]
        public string? Site { get; set; }
    }
}
