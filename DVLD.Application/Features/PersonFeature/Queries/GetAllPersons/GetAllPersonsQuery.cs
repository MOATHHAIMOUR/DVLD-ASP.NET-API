using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.People;
using DVLD.Domain.Entites;
using DVLD.Domain.views;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Queries.GetAllPersons
{
    public class GetAllPersonsQuery : IRequest<ApiResponse<List<PeopleView>>>
    {
        public PeopleSearchParamsDTO PeopleSearchParams { get; set; }

        public GetAllPersonsQuery(PeopleSearchParamsDTO peopleSearchParams)
        {
            PeopleSearchParams = peopleSearchParams;
        }    
    }
}
