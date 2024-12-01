using AutoMapper;
using Hospital.Domain.Entities;
using Hospital.Domain.Models;

namespace Hospital.Domain.Profiles
{
    public class TownProfile : Profile
    {
        public TownProfile()
        {
            CreateMap<Town, TownModel>();
        }
    }
}
