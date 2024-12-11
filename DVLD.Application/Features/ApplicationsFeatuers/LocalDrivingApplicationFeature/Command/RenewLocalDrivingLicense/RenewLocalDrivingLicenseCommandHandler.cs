using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.RenewLocalDrivingLicense;
using DVLD.Application.Services.IServices;
using MediatR;

namespace DVLD.Application.Features.LocalDrivingApplicationFeature.Command.RenewLocalDrivingLicense
{
    public class RenewLocalDrivingLicenseCommandHandler : IRequestHandler<RenewLocalDrivingLicenseCommand, ApiResponse<RenewLocalLicenseResultDTO>>
    {
        private readonly ILocalDrivingLicenseApplicationServices _localDrivingLicenseApplicationServices;

        public RenewLocalDrivingLicenseCommandHandler(ILocalDrivingLicenseApplicationServices localDrivingLicenseApplicationServices)
        {
            _localDrivingLicenseApplicationServices = localDrivingLicenseApplicationServices;
        }

        public async Task<ApiResponse<RenewLocalLicenseResultDTO>> Handle(RenewLocalDrivingLicenseCommand request, CancellationToken cancellationToken)
        {
            var renewLocalLicenseResultDTO = await _localDrivingLicenseApplicationServices.RenewLocalDrivingLicenseAsync(request.LicenseId, request.CreatedByUserId, request.ExpirationDate);

            if (!renewLocalLicenseResultDTO.IsSuccess)
                return ApiResponseHandler.BadRequest<RenewLocalLicenseResultDTO>([renewLocalLicenseResultDTO.Error.Message]);


            return ApiResponseHandler.Success(renewLocalLicenseResultDTO.Value!);
        }
    }
}
