using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.views.Test;
using MediatR;

namespace DVLD.Application.Features.TestFeatuer.Queries.GetTestLocalApplicationDetail
{
    public class GetTestLocalDrivingLicenseApplicationDetailQueryHandler : IRequestHandler<GetTestLocalDrivingLicenseApplicationDetailQuery, ApiResponse<TestLocalApplicationView>>
    {

        private ITestServices _testServices;

        public GetTestLocalDrivingLicenseApplicationDetailQueryHandler(ITestServices testServices)
        {
            _testServices = testServices;
        }

        public async Task<ApiResponse<TestLocalApplicationView>> Handle(GetTestLocalDrivingLicenseApplicationDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _testServices.GetTestLocalApplicationDetial(request.LocalDrivingLicenseApplicationId);

            if (result.IsSuccess)
                return ApiResponseHandler.Success(result.Value!);
            else
                return ApiResponseHandler.NotFound<TestLocalApplicationView>([result.Error.Message]);
        }
    }
}
