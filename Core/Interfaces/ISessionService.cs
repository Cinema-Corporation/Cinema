using BusinessLogic.DTOs;
namespace BusinessLogic.Interfaces;

public interface ISessionService
{
    IEnumerable<SessionDTO> GetActiveSessions();
    IEnumerable<SessionWithMovieDTO> GetActiveSessionsWithMovies();
}
