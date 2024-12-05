using AutoMapper;
using DVLD.Domain.Entites;

namespace DVLD.Application.DTO.TestDTOs.TestProfiler
{
    public class TestProfiler : Profile
    {
        public TestProfiler()
        {
            CreateMap<AddTestAppintmentDTO, TestAppointment>();
            CreateMap<AddTestDTO, Test>();

        }
    }
}
