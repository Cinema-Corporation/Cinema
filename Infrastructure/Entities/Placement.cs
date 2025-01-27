using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;

namespace DataAccess.Entities;

public class Placement : IEntity
{
    [Column("PlacementId")]
    public int Id { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public bool Luxe { get; set; }
}
