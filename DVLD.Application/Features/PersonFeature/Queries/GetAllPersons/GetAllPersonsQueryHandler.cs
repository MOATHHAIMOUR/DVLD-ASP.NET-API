using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Common.Helpers;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using DVLD.Domain.Enums;
using DVLD.Domain.views;
using MediatR;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DVLD.Application.Features.PersonFeature.Queries.GetAllPersons
{
    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllPersonsQuery, ApiResponse<List<PeopleView>>>
    {
        private readonly IPersonServices _personServices;

        public GetAllCountriesQueryHandler(IPersonServices personServices)
        {
            _personServices = personServices;
        }

        public async Task<ApiResponse<List<PeopleView>>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            // Fluent Validation has already ensured Gender is either null or a valid EnumGender value.
            EnumGender? gender = string.IsNullOrEmpty(request.PeopleSearchParams.Gender)
                ? null
                : Enum.Parse<EnumGender>(request.PeopleSearchParams.Gender);

            // Since OrderDirection is validated, assume it's "ASC" or "DESC" if not null.
            EnumSortDirection? sortDirection = string.IsNullOrEmpty(request.PeopleSearchParams.SortDirection) ?
                EnumSortDirection.ASC :
                Enum.Parse<EnumSortDirection>(request.PeopleSearchParams.SortDirection);

            var peopel = (await _personServices.GetAllPeopleViewAsync(
                request.PeopleSearchParams.PersonId,
                request.PeopleSearchParams.NationalNo,
                request.PeopleSearchParams.FirstName,
                request.PeopleSearchParams.SecondName,
                request.PeopleSearchParams.ThirdName,
                request.PeopleSearchParams.LastName,
                gender,
                request.PeopleSearchParams.Phone,
                request.PeopleSearchParams.Email,
                request.PeopleSearchParams.CountryName
             
                )).Value;

            return ApiResponseHandler.Success(peopel ?? [], meta: new
            {
                count = peopel?.Count ?? 0

            });
        }
    }
}
