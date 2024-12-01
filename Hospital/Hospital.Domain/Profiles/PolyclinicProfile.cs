using AutoMapper;
using Hospital.Domain.Entities;
using Hospital.Domain.Models;

namespace Hospital.Domain.Profiles;

public class PolyclinicProfile : Profile
{
    public PolyclinicProfile()
    {
        CreateMap<Polyclinic, PolyclinicModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForPath(dest => dest.TownId, opt => opt.MapFrom(src => src.TownId))
            .ForMember(dest => dest.TownName, opt => opt.MapFrom(src => src.Town!.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(src => src.ContactNumber))
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo));
    }
}
