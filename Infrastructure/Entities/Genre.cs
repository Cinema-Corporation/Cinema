using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;

namespace DataAccess.Entities;

public class Genre : IEntity
{
    [Column("GenreId")]
    public int Id { get; set; }
    public string? Name { get; set; }
}
