using DVLD.Application.Common.ApiResponse;
using DVLD.Domain.views.Test;
using MediatR;

namespace DVLD.Application.Features.TestFeatuer.Queries.GetTestAppointmentDetailInfo
{
    public class GetTestAppointmentsQuery : IRequest<ApiResponse<IEnumerable<TestAppointmentView>>>
    {
        public GetTestAppointmentsQuery(int localDrivingLicenseApplicationId, int testTypeId)
        {
            LocalDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            TestTypeId = testTypeId;
        }

        public int LocalDrivingLicenseApplicationId { get; set; }
        public int TestTypeId { get; set; }



    }
}
