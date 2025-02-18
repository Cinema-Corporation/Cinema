using DataAccess.Entities;
namespace WebApp.ViewModels;

public class SessionsMoviesViewModel
{
    public IEnumerable<Movie>? Movies { get; set; }
    public IEnumerable<Session>? Sessions { get; set; }
}
