namespace BusinessLogic.DTOs;

public class TicketDTO
{
    public int TicketId { get; set; }
    public int SessionId { get; set; }
    public int PlaceId { get; set; }
    public string UserId { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime PaymentDate { get; set; }
}
