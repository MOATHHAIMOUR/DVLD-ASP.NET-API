using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Features.TestFeatuer.Queries.GetScheduleTestInfoAsync;
using DVLD.Application.Services.IServices;
using DVLD.Domain.views.Test;
using MediatR;

namespace DVLD.Application.Features.TestFeatuer.Queries.GetScheduleTestInfo
{
    public class GetScheduleTestInfoQueryHandler : IRequestHandler<GetScheduleTestInfoQuery, ApiResponse<ScheduleAndTake_TestView>>
    {
        private readonly ITestServices _testServices;

        public GetScheduleTestInfoQueryHandler(ITestServices testServices)
        {
            _testServices = testServices;
        }

        public async Task<ApiResponse<ScheduleAndTake_TestView>> Handle(GetScheduleTestInfoQuery request, CancellationToken cancellationToken)
        {
            var result = await _testServices.GetScheduleTestInfoAsync(request.LocalDrivingLicenseApplication, request.TestTypeId);
            if (result == null)
                return ApiResponseHandler.NotFound<ScheduleAndTake_TestView>([result.Error.Message]);

            return ApiResponseHandler.Success(result.Value!);
        }
    }
}
