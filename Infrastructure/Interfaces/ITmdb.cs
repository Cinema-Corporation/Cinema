using DataAccess.Entities;
using DataAccess.Tmdb;

public interface ITmdb
{
    public Task<List<MovieSearchItem>> SearchMoviesAsync(string query);
    public Task<MovieSearchItem> GetMovieDetailsAsync(int movieId);
    public Task SaveMovieGenresToDatabaseAsync(Movie movie);
}
