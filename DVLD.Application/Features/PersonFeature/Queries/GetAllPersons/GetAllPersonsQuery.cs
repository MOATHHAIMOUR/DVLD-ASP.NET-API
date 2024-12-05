using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.PersonDtos;
using DVLD.Domain.views.Person;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Queries.GetAllPersons
{
    public class GetAllPersonsQuery : IRequest<ApiResponse<IEnumerable<PeopleView>>>
    {
        public PeopleSearchParamsDTO PeopleSearchParams { get; set; }

        public GetAllPersonsQuery(PeopleSearchParamsDTO peopleSearchParams)
        {
            PeopleSearchParams = peopleSearchParams;
        }
    }
}
