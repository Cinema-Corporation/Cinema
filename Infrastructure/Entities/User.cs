using DataAccess.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess.Entities;

public class User : IEntity
{
    [Column("UserId")]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
