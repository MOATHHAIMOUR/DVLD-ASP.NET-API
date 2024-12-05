using DVLD.Application.Common.ApiResponse;
using DVLD.Domain.views.Test;
using MediatR;

namespace DVLD.Application.Features.TestFeatuer.Queries.GetScheduleTestInfoAsync
{
    public class GetScheduleTestInfoQuery : IRequest<ApiResponse<ScheduleTestView>>
    {
        public GetScheduleTestInfoQuery(int localDrivingLicenseApplication, int testTypeId)
        {
            LocalDrivingLicenseApplication = localDrivingLicenseApplication;
            TestTypeId = testTypeId;
        }

        public int LocalDrivingLicenseApplication { get; set; }
        public int TestTypeId { get; set; }
    }
}
