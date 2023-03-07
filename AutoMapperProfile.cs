using AutoMapper;
using UserManagement_Serv.Dto;
using UserManagement_Serv.Models;

namespace UserManagement_Serv
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
