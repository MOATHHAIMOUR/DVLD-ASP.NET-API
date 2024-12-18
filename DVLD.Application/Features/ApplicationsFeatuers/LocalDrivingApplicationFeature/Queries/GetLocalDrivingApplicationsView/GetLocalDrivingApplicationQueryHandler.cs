using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.views.LocalDrivingApplication;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Queries.GetLocalDrivingApplicationsView
{
    public class GetLocalDrivingApplicationQueryHandler : IRequestHandler<GetLocalDrivingApplicationViewQuery, ApiResponse<IEnumerable<LocalDrivingApplicationView>>>
    {
        private readonly ILocalDrivingLicenseApplicationServices _localDrivingApplicationServices;

        public GetLocalDrivingApplicationQueryHandler(ILocalDrivingLicenseApplicationServices localDrivingApplicationServices)
        {
            _localDrivingApplicationServices = localDrivingApplicationServices;
        }

        public async Task<ApiResponse<IEnumerable<LocalDrivingApplicationView>>> Handle(GetLocalDrivingApplicationViewQuery request, CancellationToken cancellationToken)
        {
            var localDrivingApplications = (await _localDrivingApplicationServices.GetLocalDrivingApplicationView(request.LocalDrivingApplicationsSearchParameters)).Value;

            // Count the number of people
            int numberOfLocalDrivingApplication = localDrivingApplications?.Count() ?? 0;

            // Calculate the total pages based on the page size
            int totalPages = (int)Math.Ceiling(numberOfLocalDrivingApplication / Convert.ToDouble(request.LocalDrivingApplicationsSearchParameters.PageSize));

            return ApiResponseHandler.Success(localDrivingApplications ?? [], meta: new
            {
                count = numberOfLocalDrivingApplication,
                totalPages,
            });
        }
    }
}
