using AutoMapper;
using DVLD.Domain.Entites;

namespace DVLD.Application.DTO.PersonDtos.Profiler;

public class PersonProfiler : Profile
{
    public PersonProfiler()
    {
        CreateMap<AddPersonDTO, Person>();

        CreateMap<UpdatePersonDTO, Person>();

        CreateMap<Person, GetPersonDTO>();
    }
}
