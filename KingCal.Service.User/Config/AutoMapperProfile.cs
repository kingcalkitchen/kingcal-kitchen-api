using AutoMapper;
using KingCal.Common.DTOs;
using KingCal.Data.Entities;

namespace KingCal.Service.User.Config
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Data.Entities.User, UserDTO>();
            CreateMap<UserDTO, Data.Entities.User>();

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();

            CreateMap<UserRole, UserRoleDTO>();
            CreateMap<UserRoleDTO, UserRole>();

        }
    }
}
