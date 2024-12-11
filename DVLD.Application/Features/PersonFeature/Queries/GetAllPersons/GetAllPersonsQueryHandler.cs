using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.views.Person;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Queries.GetAllPersons
{
    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, ApiResponse<IEnumerable<PersonView>>>
    {
        private readonly IPersonServices _personServices;
        private readonly ISharedServices _sharedServices;

        public GetAllPersonsQueryHandler(IPersonServices personServices, ISharedServices sharedServices)
        {
            _personServices = personServices;
            _sharedServices = sharedServices;
        }

        public async Task<ApiResponse<IEnumerable<PersonView>>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            // people view data
            IEnumerable<PersonView> peopel = (await _personServices.GetAllPeopleViewAsync(request.PeopleSearchParams)).Value!;

            // get total people number 
            int totalNumberOfPeople = (await _sharedServices.GetRowCountAsync("People")).Value;


            // Calculate the total pages based on the page size
            int totalPages = (int)Math.Ceiling(totalNumberOfPeople / Convert.ToDouble(request.PeopleSearchParams.PageSize));

            return ApiResponseHandler.Success(peopel, meta: new
            {
                count = totalNumberOfPeople,
                totalPages,
            });
        }
    }
}
