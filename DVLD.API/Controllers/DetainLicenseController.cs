using DVLD.API.AppMetaData;
using DVLD.API.Controllers.Base;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.DetainLicenseDtos;
using DVLD.Application.DTO.InternationalLicenseDtos;
using DVLD.Application.Features.ApplicationsFeatuers.DetainLicenseFeatuer.Commands.DetainLocalDrivingLicense;
using DVLD.Application.Features.ApplicationsFeatuers.DetainLicenseFeatuer.Commands.ReleaseLocalDrivingLicense;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers
{
    public class DetainLicenseController : AppController
    {
        public DetainLicenseController(IMediator mediator) : base(mediator)
        {
        }


        [HttpPost(Router.DetainLicenseRouting.DetainLicense)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<InternationalLicenseResultDTO>>> DetainLicense([FromBody] AddNewDetainLicenseDTO addNewDetainLicenseDTO)
        {
            var response = await _mediator.Send(new DetainLocalDrivingLicenseCommand(addNewDetainLicenseDTO));
            return NewResult(response);
        }

        [HttpPost(Router.DetainLicenseRouting.ReleaseLicense)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<InternationalLicenseResultDTO>>> ReleaseLicense([FromBody] LicenseReleaseDTO LicenseReleaseDTO)
        {
            var response = await _mediator.Send(new ReleaseLocalDrivingLicenseCommand(LicenseReleaseDTO));
            return NewResult(response);
        }

    }
}
