using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using DVLD.Domain.views.LocalDrivingApplication;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Queries.GetLocalDrivingApplicationsView
{
    public class GetLocalDrivingApplicationViewQuery : IRequest<ApiResponse<IEnumerable<LocalDrivingApplicationView>>>
    {
        public SearchLocalDrivingApplicationViewDto SearchLocalDrivingApplicationViewDto { set; get; }
        public GetLocalDrivingApplicationViewQuery(SearchLocalDrivingApplicationViewDto searchLocalDrivingApplicationViewDto)
        {
            SearchLocalDrivingApplicationViewDto = searchLocalDrivingApplicationViewDto;
        }

    }
}
