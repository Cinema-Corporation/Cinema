using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;

namespace DataAccess.Entities;

public class MovieGenre : IEntity
{
    [Column("MovieId")]
    public int Id { get; set; }
    public int Genre { get; set; }
}
