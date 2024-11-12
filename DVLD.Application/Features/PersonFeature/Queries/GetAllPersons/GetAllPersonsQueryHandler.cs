using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Common.Helpers;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using DVLD.Domain.views;
using MediatR;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DVLD.Application.Features.PersonFeature.Queries.GetAllPersons
{
    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, ApiResponse<List<PeopleView>>>
    {
        private readonly IPersonServices _personServices;

        public GetAllPersonsQueryHandler(IPersonServices personServices)
        {
            _personServices = personServices;
        }

        public async Task<ApiResponse<List<PeopleView>>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {

            var peopel = (await _personServices.GetAllPeopleViewAsync(
                request.PeopleSearchParams.PersonId,
                request.PeopleSearchParams.NationalNo,
                request.PeopleSearchParams.FirstName,
                request.PeopleSearchParams.SecondName,
                request.PeopleSearchParams.ThirdName,
                request.PeopleSearchParams.LastName,
                request.PeopleSearchParams.Gender,
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
