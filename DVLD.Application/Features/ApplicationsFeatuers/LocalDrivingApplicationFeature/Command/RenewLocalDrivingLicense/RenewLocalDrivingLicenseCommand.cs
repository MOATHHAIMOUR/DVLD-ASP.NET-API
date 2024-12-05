using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.RenewLocalDrivingLicense
{
    public class RenewLocalDrivingLicenseCommand : IRequest<ApiResponse<RenewLocalLicenseResultDTO>>
    {
        public RenewLocalDrivingLicenseCommand(int licenseId, int createdByUserId, DateTime expirationDate)
        {
            LicenseId = licenseId;
            CreatedByUserId = createdByUserId;
            ExpirationDate = expirationDate;
        }

        public int LicenseId { set; get; }
        public int CreatedByUserId { set; get; }
        public DateTime ExpirationDate { set; get; }
    }
}
