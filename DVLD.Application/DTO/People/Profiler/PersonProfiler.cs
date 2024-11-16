using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.Enums;

namespace DVLD.Application.DTO.People.Profiler
{
    public class PersonProfiler : Profile
    {
        public PersonProfiler() {

            CreateMap<AddPersonDTO, Person>();

            CreateMap<UpdatePersonDTO, Person>();

            CreateMap<Person,PersonDTO>()
                .ForMember(dest => dest.Gender,
                           opt => opt.MapFrom(src => src.Gender == EnumGender.Male ? "male" : "female"));
        }
    }
}
