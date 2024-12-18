using AutoMapper;
using DVLD.Domain.Entites;
using System.Data;

namespace DVLD.Infrastructure.RepositoryProfiler
{
    public class SharedSqlProfiler : Profile
    {
        public SharedSqlProfiler()
        {
            CreateMap<IDataRecord, LicenseClass>()
            .ForMember(dest => dest.LicenseClassId, opt => opt.MapFrom(src => Convert.ToInt32(src["LicenseClassId"])))
            .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src["ClassName"].ToString()))
            .ForMember(dest => dest.ClassDescription, opt => opt.MapFrom(src => src["ClassDescription"].ToString()))
            .ForMember(dest => dest.MinimumAllowedAge, opt => opt.MapFrom(src => Convert.ToInt32(src["MinimumAllowedAge"])))
            .ForMember(dest => dest.DefaultValidityLength, opt => opt.MapFrom(src => Convert.ToInt32(src["DefaultValidityLength"])))
            .ForMember(dest => dest.ClassFees, opt => opt.MapFrom(src => Convert.ToDecimal(src["ClassFees"])));
        }
    }
}
