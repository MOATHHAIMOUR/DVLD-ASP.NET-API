using DVLD.Application.Common.ApiResponse;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Queries.GetAllPersons
{
    public class GetAllPersonsQuery : IRequest<ApiResponse<List<Person>>>
    {
        
    }
}
