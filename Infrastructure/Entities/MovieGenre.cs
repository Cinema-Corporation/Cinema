using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;

namespace DataAccess.Entities;

public class MovieGenre : IEntity
{
    [Column("MovieGenreId")]
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int GenreId { get; set; }
}
