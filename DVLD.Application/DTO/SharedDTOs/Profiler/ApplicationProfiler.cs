using AutoMapper;
using DVLD.Domain.Entites;

namespace DVLD.Application.DTO.SharedDTOs.Profiler
{
    public class ApplicationProfiler : Profile
    {
        public ApplicationProfiler()
        {
            CreateMap<UpdateApplicationTypeDTO, ApplicationType>();
        }
    }
}
