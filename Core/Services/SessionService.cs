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

    public SessionService(IMapper mapper,IRepository<Session> sessionRepository)
    {
        _mapper = mapper;
        _sessionRepository = sessionRepository;
    }

    public List<SessionDTO> GetAllSessions()
    {
        var result = _sessionRepository.GetAll();
        return _mapper.Map<List<SessionDTO>>(result);
    }
}
