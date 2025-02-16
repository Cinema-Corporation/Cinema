using DataAccess.Entities;
using DataAccess.Tmdb;

namespace DataAccess.Interfaces
{
    public interface ITmdb
    {
        public Task<List<MovieSearchItem>> SearchMoviesAsync(string query);
        public Task<MovieSearchItem> GetMovieDetailsAsync(int movieId);
        public Task SaveMovieGenresToDatabaseAsync(Movie movie);

    }
}
