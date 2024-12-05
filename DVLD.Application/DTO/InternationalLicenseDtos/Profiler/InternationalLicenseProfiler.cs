using AutoMapper;
using DVLD.Domain.Entites;

namespace DVLD.Application.DTO.InternationalLicenseDtos.Profiler
{
    public class InternationalLicenseProfiler : Profile
    {
        public InternationalLicenseProfiler()
        {
            CreateMap<AddNewInternationalLicenseDTO, InternationalLicense>();
            CreateMap<InternationalLicense, GetInternationalLicenseDTO>();

        }
    }
}
