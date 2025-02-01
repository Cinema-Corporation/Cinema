using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces;

public interface ISessionService
{
    List<SessionDTO> GetAllSessions();
}
