using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.DetainLicenseFeatuer.Commands.DetainLocalDrivingLicense
{
    public class DetainLocalDrivingLicenseCommand : IRequest<ApiResponse<RenewLocalLicenseResultDTO>>
    {
        public DetainLocalDrivingLicenseCommand(DetainLicenseDTO detainLicenseDTO)
        {
            DetainLicenseDTO = detainLicenseDTO;
        }

        public DetainLicenseDTO DetainLicenseDTO { get; set; }

    }
}
