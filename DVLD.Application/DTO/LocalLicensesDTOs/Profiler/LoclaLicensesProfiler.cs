using AutoMapper;
using DVLD.Domain.Entites;

namespace DVLD.Application.DTO.LocalLicensesDTOs.Profiler
{
    public class LoclaLicensesProfiler : Profile
    {
        public LoclaLicensesProfiler()
        {
            CreateMap<AddLocalLicensesDTO, License>();
        }
    }
}
