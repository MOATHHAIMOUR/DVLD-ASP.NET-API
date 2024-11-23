using AutoMapper;
using DVLD.Domain.Entites;

namespace DVLD.Application.DTO.Users.Profiler
{
    public class UserProfiler : Profile
    {
        public UserProfiler()
        {
            CreateMap<User, GetUserDTO>();
            CreateMap<AddUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();


        }
    }
}
