using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.views.License.InternationalLicense;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.InternationalLicenseFeature.Quiers.GetInternationalLicense
{
    internal class GetInternationalLicenseQueryHandler : IRequestHandler<GetInternationalLicenseQuery, ApiResponse<InternationalLicenseView>>
    {
        private readonly IInternationalLicenseServices _internationalLicenseServices;

        public GetInternationalLicenseQueryHandler(IInternationalLicenseServices internationalLicenseServices)
        {
            _internationalLicenseServices = internationalLicenseServices;
        }

        public async Task<ApiResponse<InternationalLicenseView>> Handle(GetInternationalLicenseQuery request, CancellationToken cancellationToken)
        {

            var result = await _internationalLicenseServices.GetInternationalLicenseViewAsync(request.InternationalLicenseId);

            if (!result.IsSuccess)
                return ApiResponseHandler.NotFound<InternationalLicenseView>([result.Error.Message]);

            return ApiResponseHandler.Success(result.Value!);

        }
    }
}
