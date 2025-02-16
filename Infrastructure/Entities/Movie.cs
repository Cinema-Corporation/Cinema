using DataAccess.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess.Entities;

public class Movie : IEntity
{
    [Column("MovieId")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    public string? Name { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
    public string? Description { get; set; }

    [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
    public float Rating { get; set; }

    [Required(ErrorMessage = "Duration is required")]
    [Range(1, 300, ErrorMessage = "Duration must be between 1 and 300 minutes")]
    public int Duration { get; set; }

    // [Url(ErrorMessage = "Invalid poster URL")]
    public string? PosterUrl { get; set; }
    
    // [Url(ErrorMessage = "Invalid trailer URL")]
    public string? TrailerUrl { get; set; }
    public bool Released { get; set; }
}