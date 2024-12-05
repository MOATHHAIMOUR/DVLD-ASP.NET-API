using DVLD.API.AppMetaData;
using DVLD.API.Controllers.Base;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.InternationalLicenseDtos;
using DVLD.Application.Features.InternationalLicenseFeature.Commands.AddNewInternationalLicense;
using DVLD.Application.Features.InternationalLicenseFeature.Quiers.GetAllInternationalLicense;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers
{
    [ApiController]
    public class InternationalLicenseController : AppController
    {
        public InternationalLicenseController(IMediator mediator) : base(mediator)
        {
        }



        [HttpPost(Router.InternationalLicenseRouting.AddNewInternationalLicense)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<InternationalLicenseResultDTO>>> AddNewInternationalLicense([FromBody] AddNewInternationalLicenseDTO AddNewInternationalLicenseDTO)
        {
            var response = await _mediator.Send(new AddNewInternationalLicenseCommand(AddNewInternationalLicenseDTO));
            return NewResult(response);
        }



        [HttpGet(Router.InternationalLicenseRouting.GetInternationalLicenseById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse<InternationalLicenseResultDTO>>> GetInternationalLicenseById([FromQuery] int internationalLicenseId)
        {
            var response = await _mediator.Send(new GetInternationalLicenseQuery(internationalLicenseId));
            return NewResult(response);
        }


        [HttpGet(Router.InternationalLicenseRouting.GetAllInternationalLicense)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse<InternationalLicenseResultDTO>>> GetAllInternationalLicenses()
        {
            var response = await _mediator.Send(new GetAllInternationalLicenseQuery());
            return NewResult(response);
        }


    }
}
