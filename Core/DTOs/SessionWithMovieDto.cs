namespace BusinessLogic.DTOs
{
    public class SessionWithMovieDTO
    {
        public SessionDTO Session { get; set; } = null!;
        public MovieDTO? Movie { get; set; }
    }
}
