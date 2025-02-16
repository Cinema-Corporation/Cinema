using DataAccess.Entities;
namespace WebApp.ViewModels;

public class EditSessionViewModel(IEnumerable<Movie> Movies, Session Session)
{
    public IEnumerable<Movie> Movies { get; set; } = Movies;
    public Session Session { get; set; } = Session;
}
