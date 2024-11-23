using AutoMapper;
using DVLD.Application.DTO.PersonDtos.Connverters;
using DVLD.Domain.Entites;

namespace DVLD.Application.DTO.PersonDtos.Profiler;

public class PersonProfiler : Profile
{
    public PersonProfiler()
    {
        CreateMap<AddPersonDTO, Person>()
                 .ForMember(dest => dest.Gender,
                 opt => opt.ConvertUsing(new GenderConverter(), src => src.Gender));

        CreateMap<UpdatePersonDTO, Person>()
            .ForMember(dest => dest.Gender,
            opt => opt.ConvertUsing(new GenderConverter(), src => src.Gender));


        CreateMap<Person, GetPersonDTO>()
            .ForMember(dest => dest.Gender,
            opt => opt.ConvertUsing(new GenderConverter(), src => src.Gender));
    }
}
