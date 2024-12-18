using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.LocalLicensesDTOs;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.AddLocalLicenses
{
    public class AddLocalLicenseCommand : IRequest<ApiResponse<string>>
    {
        public AddLocalLicenseCommand(AddLocalLicensesDTO addNewDetainLicenseDTO)
        {
            AddNewDetainLicenseDTO = addNewDetainLicenseDTO;
        }

        public AddLocalLicensesDTO AddNewDetainLicenseDTO { get; set; }
    }
}
