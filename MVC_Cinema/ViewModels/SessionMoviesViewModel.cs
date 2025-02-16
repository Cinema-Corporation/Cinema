using DataAccess.Entities;
namespace WebApp.ViewModels;

public class SessionMoviesViewModel
{
    public IEnumerable<Movie>? Movies { get; set; }
    public required Session Session { get; set; }
}
