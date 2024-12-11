using AutoMapper;
using DVLD.Domain.Entites;

namespace DVLD.Application.DTO.DetainLicenseDtos.Profiler
{
    public class DetainLicenseProfiler : Profile
    {
        public DetainLicenseProfiler() { CreateMap<AddNewDetainLicenseDTO, DetainedLicense>(); }
    }
}
