using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.views.Test;
using MediatR;

namespace DVLD.Application.Features.TestFeatuer.Queries.GetTestAppointmentDetailInfo
{
    public class GetTestLocalDrivingLicenseApplicationDetailQueryHandler : IRequestHandler<GetTestLocalDrivingLicenseApplicationDetailQuery, ApiResponse<TestAppointmentDetailInfo>>
    {

        private ITestServices _testServices;

        public GetTestLocalDrivingLicenseApplicationDetailQueryHandler(ITestServices testServices)
        {
            _testServices = testServices;
        }

        public async Task<ApiResponse<TestAppointmentDetailInfo>> Handle(GetTestLocalDrivingLicenseApplicationDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _testServices.GetTestAppointmentView(request.LocalDrivingLicenseApplicationId);

            if (result.IsSuccess)
                return ApiResponseHandler.Success(result.Value!);
            else
                return ApiResponseHandler.NotFound<TestAppointmentDetailInfo>([result.Error.Message]);
        }
    }
}
