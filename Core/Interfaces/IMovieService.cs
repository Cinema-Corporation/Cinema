﻿using BusinessLogic.DTOs;
namespace BusinessLogic.Interfaces;

public interface IMovieService
{
    IEnumerable<MovieDTO> GetMoviesByIds(IEnumerable<int> movieIds);
    MovieDTO GetMovieById(int movieId);
}
