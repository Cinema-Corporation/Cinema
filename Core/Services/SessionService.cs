using DataAccess.Entities;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using AutoMapper;

namespace BusinessLogic.Services;

public class SessionService : ISessionService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Session> _sessionRepository;

    private readonly IMovieService _movieService;

    public SessionService(IMapper mapper,
        IRepository<Session> sessionRepository,
        IMovieService movieService)
    {
        _mapper = mapper;
        _sessionRepository = sessionRepository;
        _movieService = movieService;
    }

    public IEnumerable<SessionDTO> GetActiveSessions()
    {
        var activeSessions = _sessionRepository
            .GetAll()
            .Where(s => s.TimeEnd > DateTime.Now)
            .ToList();

        return _mapper.Map<IEnumerable<SessionDTO>>(activeSessions);
    }

    public IEnumerable<SessionWithMovieDTO> GetActiveSessionsWithMovies()
    {
        var activeSessionDTOs = GetActiveSessions();

        var movieIds = activeSessionDTOs
            .Select(s => s.MovieId)
            .Distinct()
            .ToList();

        var movieDTOs = _movieService.GetMoviesByIds(movieIds);

        var result = activeSessionDTOs.Select(sessionDto => 
            {
                var movie = movieDTOs.FirstOrDefault(m => m.MovieId == sessionDto.MovieId);
                return new SessionWithMovieDTO
                {
                    Session = sessionDto,
                    Movie = movie
                };
            })
            .ToList();

        return result;
    }
}
