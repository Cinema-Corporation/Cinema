using DataAccess.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccess.Entities;

public class Hall : IEntity
{
    [Column("HallId")]
    public int Id { get; set; }
    public int RowsCount { get; set; }
    public int ColumnsCount { get; set; }
}
