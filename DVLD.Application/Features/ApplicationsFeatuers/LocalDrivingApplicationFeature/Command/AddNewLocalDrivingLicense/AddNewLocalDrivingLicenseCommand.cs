using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.AddNewLocalDrivingLicense
{
    public class AddNewLocalDrivingLicenseCommand : IRequest<ApiResponse<string>>
    {
        public AddNewLocalDrivingLicenseDTO AddNewLocalDrivingLicenseDTO { get; set; }
        public AddNewLocalDrivingLicenseCommand(AddNewLocalDrivingLicenseDTO addNewLocalDrivingLicenseDTO)
        {
            AddNewLocalDrivingLicenseDTO = addNewLocalDrivingLicenseDTO;
        }
    }
}
