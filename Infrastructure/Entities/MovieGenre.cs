using DataAccess.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess.Entities;

public class MovieGenre : IEntity
{
    [Column("MovieId")]
    public int Id { get; set; }
    public int Genre { get; set; }
}
