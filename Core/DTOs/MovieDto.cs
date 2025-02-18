namespace BusinessLogic.DTOs;

public class MovieDTO
{
    public int MovieId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float Rating { get; set; }
    public int Duration { get; set; }
    public string? PosterUrl { get; set; }
    public string? TrailerUrl { get; set; }
    public bool Released { get; set; }

    public string GetFormattedDuration()
    {
        var hours = Duration / 60;
        var minutes = Duration % 60;
        return $"{hours}h {minutes}m";
    }
}
