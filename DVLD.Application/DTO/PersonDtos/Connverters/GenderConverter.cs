using AutoMapper;
using DVLD.Domain.Enums;

namespace DVLD.Application.DTO.PersonDtos.Connverters
{
    public class GenderConverter : IValueConverter<EnumGender, string>, IValueConverter<string, EnumGender>
    {
        // Convert EnumGender → string
        public string Convert(EnumGender sourceMember, ResolutionContext context)
        {
            return sourceMember == EnumGender.Male ? "male" : "female";
        }

        // Convert string → EnumGender
        public EnumGender Convert(string sourceMember, ResolutionContext context)
        {
            return sourceMember.ToLower() == "male" ? EnumGender.Male : EnumGender.Female;
        }
    }


}
