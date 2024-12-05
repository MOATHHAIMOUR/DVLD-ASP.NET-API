using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.views.Person;
using MediatR;

namespace DVLD.Application.Features.PersonFeature.Queries.GetAllPersons
{
    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllPersonsQuery, ApiResponse<IEnumerable<PeopleView>>>
    {
        private readonly IPersonServices _personServices;

        public GetAllCountriesQueryHandler(IPersonServices personServices)
        {
            _personServices = personServices;
        }

        public async Task<ApiResponse<IEnumerable<PeopleView>>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<PeopleView> peopel = (await _personServices.GetAllPeopleViewAsync(request.PeopleSearchParams)).Value!;

            // Count the number of people
            int numberOfPeople = peopel.Count();

            // Calculate the total pages based on the page size
            int totalPages = (int)Math.Ceiling(numberOfPeople / Convert.ToDouble(request.PeopleSearchParams.PageSize));

            return ApiResponseHandler.Success(peopel, meta: new
            {
                count = numberOfPeople,
                totalPages,
            });
        }
    }
}
