using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;

namespace DataAccess.Entities;

public class Ticket : IEntity
{
    [Column("TicketId")]
    public int Id { get; set; }
    public int SessionId { get; set; }
    public int PlaceId { get; set; }
    public decimal Price { get; set; }
    public DateTime PaymentDate { get; set; }
}
