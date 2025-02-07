using DataAccess.Tmdb;
namespace DataAccess.Interfaces;

public interface ITmdb
{
    public Task<List<MovieSearchItem>> GetLatestMoviesAsync();
}
