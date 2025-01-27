using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;

namespace DataAccess.Entities;

public class TicketDetails : IEntity
{
    [Column("TicketId")]
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int PlaceId { get; set; }
}
