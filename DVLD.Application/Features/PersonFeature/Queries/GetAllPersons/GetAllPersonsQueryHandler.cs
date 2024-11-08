using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Queries.GetAllPersons
{
    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, ApiResponse<List<Person>>>
    {
        private readonly IPersonServices _personServices;

        public GetAllPersonsQueryHandler(IPersonServices personServices)
        {
            _personServices = personServices;
        }

        public async Task<ApiResponse<List<Person>>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {

            var peopel = (await _personServices.GetAllPersonsAsync()).Value;

            return ApiResponseHandler.Success(peopel ?? [], meta: new
            {
                count = peopel?.Count ?? 0
            });
        }
    }
}
