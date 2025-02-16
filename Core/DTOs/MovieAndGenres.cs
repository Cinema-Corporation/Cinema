using DataAccess.Entities;
namespace BusinessLogic.DTOs;

public class MovieAndGenres
{
    public required Movie Movie { get; set; }
    public required List<Genre> Genres { get; set; } // all availible genres
    public required List<MovieGenre> MovieGenres { get; set; } 

}
