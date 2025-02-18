using Newtonsoft.Json;
namespace DataAccess.Tmdb;

public class MovieRunTime
{
    [JsonProperty("runtime")]
    public int Runtime { get; set; }
}
