using DVLD.API.AppMetaData;
using DVLD.API.Controllers.Base;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.InternationalLicenseDtos;
using DVLD.Application.DTO.ReplaceDamageLostLicenseDTO;
using DVLD.Application.Features.ApplicationsFeatuers.ReplaceDamageLostLicenseFeature.Commands.ReplaceDamageLicense;
using DVLD.Application.Features.ApplicationsFeatuers.ReplaceDamageLostLicenseFeature.Commands.ReplaceLostLicense;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers
{
    public class ReplaceLostDamageController : AppController
    {
        public ReplaceLostDamageController(IMediator mediator) : base(mediator)
        {
        }


        [HttpPost(Router.ReplaceLostDamageRouter.ReplaceLostLicense)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<InternationalLicenseResultDTO>>> ReplaceLostLicense([FromBody] ReplaceDamageLostDTO ReplaceDamageLostDTO)
        {
            var response = await _mediator.Send(new ReplaceLostLicenseCommand(ReplaceDamageLostDTO));
            return NewResult(response);
        }

        [HttpPost(Router.ReplaceLostDamageRouter.ReplaceDamageLicense)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ApiResponse<InternationalLicenseResultDTO>>> ReplaceDamageLicense([FromBody] ReplaceDamageLostDTO ReplaceDamageLostDTO)
        {
            var response = await _mediator.Send(new ReplaceDamageLicenseCommand(ReplaceDamageLostDTO));
            return NewResult(response);
        }

    }
}
