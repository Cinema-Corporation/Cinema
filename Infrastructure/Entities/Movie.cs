using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;

namespace DataAccess.Entities;

public class Movie : IEntity
{
    [Column("MovieId")]
    public int Id { get; set; }
    public string? Name { get; set; }
    public int GenreId { get; set; }
    public int Duration { get; set; }
}
