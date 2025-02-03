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
}
