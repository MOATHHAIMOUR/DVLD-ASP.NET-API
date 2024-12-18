using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.AddNewLocalDrivingLicenseApplication
{
    public class AddNewLocalDrivingLicenseApplicationCommandHandler : IRequest<ApiResponse<string>>
    {
        public AddNewLocalDrivingLicenseApplicationDTO AddNewLocalDrivingLicenseDTO { get; set; }
        public AddNewLocalDrivingLicenseApplicationCommandHandler(AddNewLocalDrivingLicenseApplicationDTO addNewLocalDrivingLicenseDTO)
        {
            AddNewLocalDrivingLicenseDTO = addNewLocalDrivingLicenseDTO;
        }
    }
}
