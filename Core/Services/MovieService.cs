using BusinessLogic.DTOs;
using DataAccess.Entities;
using DataAccess.Interfaces;
using BusinessLogic.Interfaces;
using AutoMapper;
namespace BusinessLogic.Services;

public class MovieService : IMovieService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Movie> _movieRepository;

    public MovieService(IRepository<Movie> movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public IEnumerable<MovieDTO> GetMoviesByIds(IEnumerable<int> movieIds)
    {
        var distinctIds = movieIds.Distinct().ToList();

        var movies = _movieRepository
            .GetAll()
            .Where(m => distinctIds.Contains(m.Id))
            .ToList();

        return _mapper.Map<IEnumerable<MovieDTO>>(movies);
    }

    public MovieDTO GetMovieById(int movieId)
    {
        var movie = _movieRepository
            .GetAll()
            .FirstOrDefault(m => m.Id == movieId);

        return _mapper.Map<MovieDTO>(movie);
    }

}
