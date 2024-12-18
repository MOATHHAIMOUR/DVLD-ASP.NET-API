using AutoMapper;
using DVLD.Domain.Entites;
using System.Data;

namespace DVLD.Infrastructure.RepositoryProfiler
{
    public class UserSqlProfiler : Profile
    {
        public UserSqlProfiler()
        {
            CreateMap<IDataReader, User>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src["UserId"]))
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src["PersonId"]))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src["Username"]))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src["Password"]))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src["IsActive"]));
        }

    }
}
