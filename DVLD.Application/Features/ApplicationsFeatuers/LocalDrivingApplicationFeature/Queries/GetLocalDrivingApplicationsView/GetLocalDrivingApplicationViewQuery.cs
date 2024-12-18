using DVLD.Application.Common.ApiResponse;
using DVLD.Domain.DomainSearchParameters;
using DVLD.Domain.views.LocalDrivingApplication;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Queries.GetLocalDrivingApplicationsView
{
    public class GetLocalDrivingApplicationViewQuery : IRequest<ApiResponse<IEnumerable<LocalDrivingApplicationView>>>
    {
        public GetLocalDrivingApplicationViewQuery(LocalDrivingApplicationsSearchParameters localDrivingApplicationsSearchParameters)
        {
            LocalDrivingApplicationsSearchParameters = localDrivingApplicationsSearchParameters;
        }

        public LocalDrivingApplicationsSearchParameters LocalDrivingApplicationsSearchParameters { set; get; }


    }
}
