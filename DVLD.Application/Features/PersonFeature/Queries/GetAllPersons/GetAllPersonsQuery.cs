using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.People;
using DVLD.Domain.Entites;
using DVLD.Domain.views;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Queries.GetAllPersons
{
    public class GetAllPersonsQuery : IRequest<ApiResponse<List<PeopleView>>>
    {
        public PeopleSearchParams PeopleSearchParams { get; set; }

        public GetAllPersonsQuery(PeopleSearchParams peopleSearchParams)
        {
            PeopleSearchParams = peopleSearchParams;
        }    
    }
}
