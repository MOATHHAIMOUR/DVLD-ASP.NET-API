using AutoMapper;
using DVLD.Domain.Entites;

namespace DVLD.Application.DTO.LocalDrivingApplicationDtos.profiler
{
    public class LocalDrivingApplicationProfiler : Profile
    {
        public LocalDrivingApplicationProfiler()
        {

            // Map LocalDrivingLicenseApplicationDto to LocalDrivingLicenseApplication
            CreateMap<AddNewLocalDrivingLicenseApplicationDTO, LocalDrivingLicenseApplication>()
                .ForMember(dest => dest.Application,
                opt => opt.MapFrom(src => new Domain.Entites.Application
                {
                    ApplicantPersonId = src.ApplicantPersonId,
                    ApplicationTypeId = src.ApplicationTypeId,
                    CreatedByUserId = src.CreatedByUserId
                }))
                .ForMember(dest => dest.LicenseClass,
                opt => opt.MapFrom(src => new LicenseClass
                {
                    LicenseClassId = (int)src.LicenseClassId,
                }));


            CreateMap<LicenseClass, GetLicenseClassDTO>();
        }
    }
}
