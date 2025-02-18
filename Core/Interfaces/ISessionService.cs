using BusinessLogic.DTOs;
namespace BusinessLogic.Interfaces;

public interface ISessionService
{
    IEnumerable<SessionDTO> GetActiveSessions();
    IEnumerable<SessionDTO> GetActiveSessionsByMovie(int movieId);
    IEnumerable<SessionWithMovieDTO> GetActiveSessionsWithMovies();
    IEnumerable<SessionWithMovieDTO> GetActiveSessionsWithMoviesByMovie(int movieId);
}
