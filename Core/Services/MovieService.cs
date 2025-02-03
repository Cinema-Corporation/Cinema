using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using BusinessLogic.Interfaces;

namespace BusinessLogic.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IMapper _mapper;

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
    }
}
