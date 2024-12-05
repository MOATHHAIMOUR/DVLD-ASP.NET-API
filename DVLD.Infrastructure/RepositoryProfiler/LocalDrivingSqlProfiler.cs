using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
using DVLD.Domain.views.LocalDrivingApplication;
using DVLD.Domain.views.Test;
using System.Data;

namespace DVLD.Infrastructure.RepositoryProfiler
{
    public class LocalDrivingSqlProfiler : Profile
    {

        public LocalDrivingSqlProfiler()
        {
            CreateMap<IDataRecord, LocalDrivingApplicationView>()
           .ForMember(dest => dest.LocalDrivingLicenseApplicationId, opt => opt.MapFrom(src =>
               src["LocalDrivingLicenseApplicationID"] != DBNull.Value ? (int?)src["LocalDrivingLicenseApplicationID"] : null))
           .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src =>
               src["ClassName"] != DBNull.Value ? src["ClassName"].ToString() : string.Empty))
           .ForMember(dest => dest.NationalNo, opt => opt.MapFrom(src =>
               src["NationalNo"] != DBNull.Value ? src["NationalNo"].ToString() : string.Empty))
           .ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
               src["FullName"] != DBNull.Value ? src["FullName"].ToString() : string.Empty))
           .ForMember(dest => dest.ApplicationDate, opt => opt.MapFrom(src =>
               src["ApplicationDate"] != DBNull.Value ? (DateTime?)src["ApplicationDate"] : null))
           .ForMember(dest => dest.PassedTests, opt => opt.MapFrom(src =>
               src["PassedTests"] != DBNull.Value ? (int?)Convert.ToInt32(src["PassedTests"]) : null))
           .ForMember(dest => dest.ApplicationStatus, opt => opt.MapFrom(src =>
               src["ApplicationStatus"] != DBNull.Value ? (EnumApplicationStatus)Convert.ToInt32(src["ApplicationStatus"]) : (EnumApplicationStatus?)null));




            CreateMap<IDataRecord, LicenseClass>()
                .ForMember(dest => dest.LicenseClassId, opt => opt.MapFrom(src =>
                    src["LicenseClassId"] != DBNull.Value ? (int?)src["LicenseClassId"] : null))
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src =>
                    src["ClassName"] != DBNull.Value ? src["ClassName"].ToString() : string.Empty))
                .ForMember(dest => dest.ClassDescription, opt => opt.MapFrom(src =>
                    src["ClassDescription"] != DBNull.Value ? src["ClassDescription"].ToString() : string.Empty))
                .ForMember(dest => dest.MinimumAllowedAge, opt => opt.MapFrom(src =>
                    src["MinimumAllowedAge"] != DBNull.Value ? (int?)src["MinimumAllowedAge"] : null))
                .ForMember(dest => dest.DefaultValidityLength, opt => opt.MapFrom(src =>
                    src["DefaultValidityLength"] != DBNull.Value ? (int?)src["DefaultValidityLength"] : null))
                .ForMember(dest => dest.ClassFees, opt => opt.MapFrom(src =>
                    src["ClassFees"] != DBNull.Value ? (decimal?)src["ClassFees"] : null));
        }
    }
}
