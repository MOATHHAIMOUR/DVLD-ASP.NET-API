using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.DetainLicenseDtos;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.DetainLicenseFeatuer.Commands.ReleaseLocalDrivingLicense
{
    public class ReleaseLocalDrivingLicenseCommand : IRequest<ApiResponse<object>>
    {
        public ReleaseLocalDrivingLicenseCommand(LicenseReleaseDTO licenseReleaseDTO)
        {
            LicenseReleaseDTO = licenseReleaseDTO;
        }

        public LicenseReleaseDTO LicenseReleaseDTO { get; set; }

    }
}
