using DVLD.API.AppMetaData;
using DVLD.API.Controllers.Base;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.LocalDrivingApplicationDtos;
using DVLD.Application.DTO.LocalLicensesDTOs;
using DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.AddLocalLicenses;
using DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.AddNewLocalDrivingLicenseApplication;
using DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.CancelLocalDrivingApplicaiton;
using DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Command.RenewLocalDrivingLicense;
using DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Queries.GetLicenseDrivingView;
using DVLD.Application.Features.ApplicationsFeatuers.LocalDrivingApplicationFeature.Queries.GetLocalDrivingApplicationsView;
using DVLD.Domain.DomainSearchParameters;
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
        public async Task<ActionResult<ApiResponse<LocalDrivingApplicationView>>> GetApplicationView([FromQuery] LocalDrivingApplicationsSearchParameters localDrivingApplicationsSearchParameters)
        {
            var response = await _mediator.Send(new GetLocalDrivingApplicationViewQuery(localDrivingApplicationsSearchParameters));
            return NewResult(response);
        }


        [HttpPost(Router.LocalDrivingApplicationRouting.AddNewLocalDrivingApplication)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<string>>> AddNewLocalDrivingApplication([FromBody] AddNewLocalDrivingLicenseApplicationDTO AddNewLocalDrivingLicenseDTO)
        {
            var response = await _mediator.Send(new AddNewLocalDrivingLicenseApplicationCommandHandler(AddNewLocalDrivingLicenseDTO));
            return NewResult(response);
        }


        [HttpPost(Router.LocalDrivingApplicationRouting.AddNewLocalLicense)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<string>>> AddNewLocalLicenses([FromBody] AddLocalLicensesDTO AddLocalLicensesDTO)
        {
            var response = await _mediator.Send(new AddLocalLicenseCommand(AddLocalLicensesDTO));
            return NewResult(response);
        }



        [HttpGet(Router.LocalDrivingApplicationRouting.GetLicenseView)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<LicenseDetailsView>>> GetLicenseView([FromQuery] int? ApplicationId, [FromQuery] int? LicenseId, [FromQuery] int? localDrivingApplicationId)
        {
            var response = await _mediator.Send(new GetLicenseDrivingViewQuery(ApplicationId, LicenseId, localDrivingApplicationId));
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



        [HttpPut(Router.LocalDrivingApplicationRouting.CancelLocalDivingApplication)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<RenewLocalLicenseResultDTO>>> CancelLocalDeivingApplication([FromQuery] int LocalDrivingApplication)
        {
            var response = await _mediator.Send(new CancelLocalDrivingApplicaitonCommand(LocalDrivingApplication));

            return NewResult(response);
        }





    }
}
