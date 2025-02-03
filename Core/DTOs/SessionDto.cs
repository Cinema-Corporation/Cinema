namespace BusinessLogic.DTOs;

public class SessionDTO
{
    public int SessionId { get; set; }
    public int MovieId { get; set; }
    public int HallId { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
}
