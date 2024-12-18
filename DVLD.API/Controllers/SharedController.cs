using DVLD.API.AppMetaData;
using DVLD.API.Controllers.Base;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.SharedDTOs;
using DVLD.Application.Features.SettingsFeatuer.Command.UpdateApplicationType;
using DVLD.Application.Features.SettingsFeatuer.Queries.GetAllApplicationTypes;
using DVLD.Application.Features.SettingsFeatuer.Queries.GetAllCountries;
using DVLD.Application.Features.SettingsFeatuer.Queries.GetLicenseClasses;
using DVLD.Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers
{
    [ApiController]
    public class SharedController : AppController
    {

        public SharedController(IMediator mediator) : base(mediator)
        {
        }


        [HttpGet(Router.SharedRouting.GetLicenseClases)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<LicenseClass>>> GetLicenseClases()
        {
            var response = await _mediator.Send(new GetLicenseClassesQuery());
            return NewResult(response);
        }


        [HttpGet(Router.SharedRouting.GetAllCountries)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<Country>>>> GetAllCountries()
        {
            var response = await _mediator.Send(new GetAllCountryQuery());
            return NewResult(response);
        }



        [HttpGet(Router.SharedRouting.GetAllApplicationTypes)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<Country>>>> GetAllApplicationTypes()
        {
            var response = await _mediator.Send(new GetAllApplicationTypesQuery());
            return NewResult(response);
        }


        [HttpPut(Router.SharedRouting.UpdateApplicationType)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<string>>> UpdateApplicationType([FromBody] UpdateApplicationTypeDTO updateApplicationTypeDTO)
        {
            var response = await _mediator.Send(new UpdateApplicationTypeCommand(updateApplicationTypeDTO));
            return NewResult(response);
        }

    }
}
