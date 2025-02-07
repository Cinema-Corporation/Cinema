using DataAccess.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess.Entities;

public class Hall : IEntity
{
    [Column("HallId")]
    public int Id { get; set; }
    public string? Capacity { get; set; }
}
