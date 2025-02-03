using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;

namespace DataAccess.Entities;

public class Hall : IEntity
{
    [Column("HallId")]
    public int Id { get; set; }
    public string? Capacity { get; set; }
}
