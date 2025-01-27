using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Entities;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<PlacementDTO, Placement>().ReverseMap();
        CreateMap<MovieDTO, Movie>().ReverseMap();
        CreateMap<GenreDTO, Genre>().ReverseMap();
        CreateMap<SessionDTO, Session>().ReverseMap();
        CreateMap<TicketDetailsDTO, TicketDetails>().ReverseMap();
        CreateMap<TicketDTO, Ticket>().ReverseMap();
        CreateMap<PaymentDTO, Payment>().ReverseMap();
        CreateMap<HallDTO, Hall>().ReverseMap();
        CreateMap<MovieSearchDTO, MovieSearchItem>().ReverseMap();
        CreateMap<UserDTO, User>().ReverseMap();
    }
}
