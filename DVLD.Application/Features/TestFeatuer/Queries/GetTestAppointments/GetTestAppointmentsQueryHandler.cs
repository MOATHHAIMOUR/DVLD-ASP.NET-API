using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.views.Test;
using MediatR;

namespace DVLD.Application.Features.TestFeatuer.Queries.GetTestAppointmentDetailInfo
{
    public class GetTestAppointmentsQueryHandler : IRequestHandler<GetTestAppointmentsQuery, ApiResponse<IEnumerable<TestAppointmentView>>>
    {

        private ITestServices _testServices;

        public GetTestAppointmentsQueryHandler(ITestServices testServices)
        {
            _testServices = testServices;
        }

        public async Task<ApiResponse<IEnumerable<TestAppointmentView>>> Handle(GetTestAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var result = await _testServices.GetTestAppointmentsPerTestType(request.LocalDrivingLicenseApplicationId, request.TestTypeId);

            return ApiResponseHandler.Success(result.Value!);

        }
    }
}
