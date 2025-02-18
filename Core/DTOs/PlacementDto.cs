namespace BusinessLogic.DTOs;

public class PlacementDTO
{
    public int PlaceId { get; set; }
    public int HallId { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public bool Luxe { get; set; }
}
