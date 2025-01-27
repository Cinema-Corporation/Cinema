using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;

namespace DataAccess.Entities;

public class Session : IEntity
{
    [Column("SessionId")]
    public int Id { get; set; }
    public int MovieId { get; set; }
    public int TicketId { get; set; }
    public int HallId { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
}
