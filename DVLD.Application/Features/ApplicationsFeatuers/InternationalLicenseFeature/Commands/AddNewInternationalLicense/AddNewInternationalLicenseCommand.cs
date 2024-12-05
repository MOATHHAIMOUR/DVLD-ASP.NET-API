using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.InternationalLicenseDtos;
using MediatR;

namespace DVLD.Application.Features.ApplicationsFeatuers.InternationalLicenseFeature.Commands.AddNewInternationalLicense
{
    public class AddNewInternationalLicenseCommand : IRequest<ApiResponse<InternationalLicenseResultDTO>>
    {
        public AddNewInternationalLicenseDTO AddNewInternationalLicenseDTO { get; set; }

        public AddNewInternationalLicenseCommand(AddNewInternationalLicenseDTO addNewInternationalLicenseDTO)
        {
            AddNewInternationalLicenseDTO = addNewInternationalLicenseDTO;
        }

    }
}
