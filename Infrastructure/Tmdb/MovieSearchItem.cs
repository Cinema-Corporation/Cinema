using Newtonsoft.Json;

namespace DataAccess.Tmdb
{
    public class MovieSearchItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("poster_path")]
        public string? PosterPath { get; set; }

        [JsonProperty("overview")]
        public string? Description { get; set; }

        [JsonProperty("vote_average")]
        public float Rating { get; set; }

        [JsonProperty("runtime")]
        public int Duration { get; set; }

        [JsonProperty("videos")]
        public MovieTrailerResult? MovieTrailerResult { get; set; }

        public string? TrailerUrl => MovieTrailerResult?.Results?
            .FirstOrDefault(v => v.Type == "Trailer" && v.Site == "YouTube")?.Key;
    }
}
