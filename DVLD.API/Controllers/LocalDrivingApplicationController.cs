using DVLD.API.AppMetaData;
using DVLD.API.Controllers.Base;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.AddNewLocalDrivingLicense;
using DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.RenewLocalDrivingLicense;
using DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Queries.GetLicenseClasses;
using DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Queries.GetLicenseDrivingView;
using DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Queries.GetLocalDrivingApplicationsView;
using DVLD.Domain.Entites;
using DVLD.Domain.views.License.LocalLicense;
using DVLD.Domain.views.LocalDrivingApplication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers
{
    [ApiController]
    public class LocalDrivingApplicationController : AppController
    {
        public LocalDrivingApplicationController(IMediator mediator) : base(mediator)
        {
        }


        [HttpGet(Router.LocalDrivingApplicationRouting.GetLocalDrivingApplicationView)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<LocalDrivingApplicationView>>> GetApplicationView([FromQuery] SearchLocalDrivingApplicationViewDto searchLocalDrivingApplicationViewDto)
        {
            var response = await _mediator.Send(new GetLocalDrivingApplicationViewQuery(searchLocalDrivingApplicationViewDto));
            return NewResult(response);
        }

        [HttpGet(Router.LocalDrivingApplicationRouting.GetLicenseClases)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<LicenseClass>>> GetLicenseClases()
        {
            var response = await _mediator.Send(new GetLicenseClassesQuery());
            return NewResult(response);
        }


        [HttpPost(Router.LocalDrivingApplicationRouting.AddNewLocalDrivingApplication)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<string>>> AddNewLocalDrivingApplication([FromBody] AddNewLocalDrivingLicenseDTO AddNewLocalDrivingLicenseDTO)
        {
            var response = await _mediator.Send(new AddNewLocalDrivingLicenseCommand(AddNewLocalDrivingLicenseDTO));
            return NewResult(response);
        }


        [HttpGet(Router.LocalDrivingApplicationRouting.GetLicenseView)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<LicenseDetailsView>>> GetLicenseView([FromQuery] int? ApplicationId, [FromQuery] int? LicenseId)
        {
            var response = await _mediator.Send(new GetLicenseDrivingViewQuery(ApplicationId, LicenseId));
            return NewResult(response);
        }



        [HttpPost(Router.LocalDrivingApplicationRouting.RenewLocalDrivingLicense)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse<RenewLocalLicenseResultDTO>>> RenewLocalDrivingLicense([FromQuery] int LicenseId, [FromQuery] int CreatedByUserId, [FromQuery] DateTime ExpirationDate)
        {
            var response = await _mediator.Send(new RenewLocalDrivingLicenseCommand(LicenseId, CreatedByUserId, ExpirationDate));

            return NewResult(response);
        }

        [HttpPost(Router.LocalDrivingApplicationRouting.DetainLocalDrivingLicense)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse<LicenseDetailsView>>> DetainLocalDrivingLicense([FromQuery] int LicenseId, [FromQuery] int CreatedByUserId, [FromQuery] DateTime ExpirationDate)
        {
            var response = await _mediator.Send(new RenewLocalDrivingLicenseCommand(LicenseId, CreatedByUserId, ExpirationDate));

            return NewResult(response);
        }


    }
}
