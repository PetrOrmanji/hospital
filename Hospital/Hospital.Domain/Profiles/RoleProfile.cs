using AutoMapper;
using Hospital.Domain.Entities;
using Hospital.Domain.Models;

namespace Hospital.Domain.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleModel>();
    }
}
