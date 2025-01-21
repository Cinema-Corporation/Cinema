using DataAccess.Interfaces;

namespace DataAccess.Entities;

public class User : IEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
