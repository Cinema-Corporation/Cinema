using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;

namespace DataAccess.Entities;

public class Payment : IEntity
{
    [Column("PaymentId")]
    public int Id { get; set; }
    public DateTime Date { get; set; }
}
