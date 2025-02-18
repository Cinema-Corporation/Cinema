using Newtonsoft.Json;
namespace DataAccess.Tmdb;

public class MovieTrailerResult
{
    [JsonProperty("results")]
    public List<Trailer>? Results { get; set; }
}
