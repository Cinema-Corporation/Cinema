using DataAccess.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess.Entities;

public class Genre : IEntity
{
    [Column("GenreId")]
    public int Id { get; set; }
    public string? Name { get; set; }
}
