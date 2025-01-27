using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;

namespace DataAccess.Entities;

public class Ticket : IEntity
{
    [Column("TicketId")]
    public int Id { get; set; }
    public int SessionId { get; set; }
    public int PaymentId { get; set; }
}
