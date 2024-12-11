using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.DetainLicenseDtos;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.DetainLicenseFeatuer.Commands.DetainLocalDrivingLicense
{
    public class DetainLocalDrivingLicenseCommand : IRequest<ApiResponse<object>>
    {
        public DetainLocalDrivingLicenseCommand(AddNewDetainLicenseDTO detainLicenseDTO)
        {
            DetainLicenseDTO = detainLicenseDTO;
        }

        public AddNewDetainLicenseDTO DetainLicenseDTO { get; set; }

    }
}
