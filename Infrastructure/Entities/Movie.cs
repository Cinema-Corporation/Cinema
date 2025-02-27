using DataAccess.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess.Entities;

public class Movie : IEntity
{
    [Column("MovieId")]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public float Rating { get; set; }
    public int Duration { get; set; }
    public string? PosterUrl { get; set; }
    public string? TrailerUrl { get; set; }
    public bool Released { get; set; }
}
