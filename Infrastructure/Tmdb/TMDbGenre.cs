using Newtonsoft.Json;
namespace DataAccess.Tmdb;

public class TMDbGenre
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }
}
