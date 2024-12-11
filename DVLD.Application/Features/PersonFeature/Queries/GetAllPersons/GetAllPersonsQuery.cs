using DVLD.Application.Common.ApiResponse;
using DVLD.Domain.DomainSearchParameters;
using DVLD.Domain.views.Person;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Queries.GetAllPersons
{
    public class GetAllPersonsQuery : IRequest<ApiResponse<IEnumerable<PersonView>>>
    {
        public PeopleSearchParameters PeopleSearchParams { get; set; }

        public GetAllPersonsQuery(PeopleSearchParameters peopleSearchParams)
        {
            PeopleSearchParams = peopleSearchParams;
        }
    }
}
