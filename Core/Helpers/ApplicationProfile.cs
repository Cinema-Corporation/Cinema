using BusinessLogic.DTOs;
using DataAccess.Entities;
using AutoMapper;
namespace BusinessLogic.Helpers;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<PlacementDTO, Placement>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PlaceId))
            .ReverseMap()
            .ForMember(dest => dest.PlaceId, opt => opt.MapFrom(src => src.Id));

        CreateMap<MovieDTO, Movie>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MovieId))
            .ReverseMap()
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.Id));

        CreateMap<GenreDTO, Genre>().ReverseMap();

        CreateMap<SessionDTO, Session>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SessionId))
            .ReverseMap()
            .ForMember(dest => dest.SessionId, opt => opt.MapFrom(src => src.Id));

        CreateMap<TicketDTO, Ticket>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TicketId))
            .ReverseMap()
            .ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.Id));

        CreateMap<HallDTO, Hall>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.HallId))
            .ReverseMap()
            .ForMember(dest => dest.HallId, opt => opt.MapFrom(src => src.Id));
    }
}
