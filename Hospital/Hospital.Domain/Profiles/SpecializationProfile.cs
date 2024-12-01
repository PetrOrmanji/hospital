using AutoMapper;
using Hospital.Domain.Entities;
using Hospital.Domain.Models;

namespace Hospital.Domain.Profiles;

public class SpecializationProfile : Profile
{
    public SpecializationProfile()
    {
        CreateMap<Specialization, SpecializationModel>();
    }
}
