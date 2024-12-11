using AutoMapper;
using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
using DVLD.Domain.views.Person;
using System.Data;

namespace DVLD.Infrastructure.RepositoryProfiler
{
    public class PersonSqlProfiler : Profile
    {
        public PersonSqlProfiler()
        {
            CreateMap<IDataRecord, Person>()
              .ForMember(
                  dest => dest.PersonId,
                  opt => opt.MapFrom(src => Convert.ToInt32(src["PersonId"]))
              )
              .ForMember(
                  dest => dest.NationalNo,
                  opt => opt.MapFrom(src => src["NationalNo"].ToString())
              )
              .ForMember(
                  dest => dest.FirstName,
                  opt => opt.MapFrom(src => src["FirstName"].ToString())
              )
              .ForMember(
                  dest => dest.SecondName,
                  opt => opt.MapFrom(src => src["SecondName"].ToString())
              )
              .ForMember(
                  dest => dest.ThirdName,
                  opt => opt.MapFrom(src => src["ThirdName"].ToString())
              )
              .ForMember(
                  dest => dest.LastName,
                  opt => opt.MapFrom(src => src["LastName"].ToString())
              )
              .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src["Gender"]))
              .ForMember(
                  dest => dest.Phone,
                  opt => opt.MapFrom(src => src["Phone"].ToString())
              )
              .ForMember(
                  dest => dest.DateOfBirth,
                  opt => opt.MapFrom(src => src["DateOfBirth"])
              )
              .ForMember(
                 dest => dest.Email,
                  opt => opt.MapFrom(src => src["Email"].ToString())
              )
              .ForMember(
                  dest => dest.CountryId,
                  opt => opt.MapFrom(src => Convert.ToInt32(src["CountryId"]))
              )
              .ForMember(
                  dest => dest.Address,
                  opt => opt.MapFrom(src => (string)src["Address"])
              )
              .ForMember(
                  dest => dest.IsUser,
                  opt => opt.MapFrom(src => (bool)src["IsUser"])
              )
              .ForMember(
                  dest => dest.ImagePath,
                  opt => opt.MapFrom(src => src["ImagePath"])
              );


            CreateMap<IDataRecord, PersonView>()
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => (int)src["PersonID"]))
            .ForMember(dest => dest.NationalNo, opt => opt.MapFrom(src => src["NationalNo"].ToString()))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src["FirstName"].ToString()))
            .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.IsDBNull(src.GetOrdinal("SecondName")) ? null : src["SecondName"].ToString()))
            .ForMember(dest => dest.ThirdName, opt => opt.MapFrom(src => src.IsDBNull(src.GetOrdinal("ThirdName")) ? null : src["ThirdName"].ToString()))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src["LastName"].ToString()))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (EnumGender)src["Gender"]))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src["Phone"].ToString()))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src["Email"].ToString()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src["Address"].ToString()))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src["DateOfBirth"].ToString()))
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src["CountryName"].ToString()));



        }
    }
}
