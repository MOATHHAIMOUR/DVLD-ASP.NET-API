using DVLD.Application.Common.ApiResponse;
using DVLD.Domain.views.Test;
using MediatR;

namespace DVLD.Application.Features.TestFeatuer.Queries.GetTestLocalApplicationDetail
{
    public class GetTestLocalDrivingLicenseApplicationDetailQuery : IRequest<ApiResponse<TestLocalApplicationView>>
    {
        public int LocalDrivingLicenseApplicationId { get; set; }
        public GetTestLocalDrivingLicenseApplicationDetailQuery(int localDrivingLicenseApplicationId)
        {
            LocalDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
        }

    }
}
