using BusinessLogic.DTOs;
using DataAccess.Tmdb;
namespace BusinessLogic.Interfaces;

public interface IAdminService
{
    public void DeleteMovie(int movieId);
    public void AddMovie(MovieAndGenres movieAndGenres);
    public Task AddSearchMovie(MovieSearchItem movie);
}
