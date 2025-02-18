using DataAccess.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess.Entities;

public class Placement : IEntity
{
    [Column("PlaceId")]
    public int Id { get; set; }
    public int HallId { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public bool Luxe { get; set; }
}
