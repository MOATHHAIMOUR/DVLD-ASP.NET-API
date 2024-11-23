using AutoMapper;
using DVLD.Domain.Enums;

namespace DVLD.Application.DTO.Resolvers
{
    public class GenderConverter : IValueConverter<string, EnumSortDirection>
    {
        public EnumSortDirection Convert(string sourceMember, ResolutionContext context)
        {
            return sourceMember == "ASC" ? EnumSortDirection.ASC : EnumSortDirection.DESC;
        }
    }

}
