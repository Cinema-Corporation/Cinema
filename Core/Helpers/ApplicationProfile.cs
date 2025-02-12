using BusinessLogic.DTOs;
using DataAccess.Entities;
using AutoMapper;
namespace BusinessLogic.Helpers;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<PlacementDTO, Placement>().ReverseMap();
        CreateMap<MovieDTO, Movie>().ReverseMap()
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MovieId)); ;
        CreateMap<GenreDTO, Genre>().ReverseMap();
        CreateMap<SessionDTO, Session>().ReverseMap();
        CreateMap<TicketDTO, Ticket>().ReverseMap();
        CreateMap<HallDTO, Hall>().ReverseMap();
    }
}
