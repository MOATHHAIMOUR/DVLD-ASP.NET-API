using DVLD.API.AppMetaData;
using DVLD.API.Controllers.Base;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Features.SettingsFeatuer.Queries.GetAllCountries;
using DVLD.Domain.Entites;
using DVLD.Domain.views;
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


        [HttpGet(Router.SharedRouting.GetAllCountries)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<List<Country>>>> GetAllCountries()
        {
            var response = await _mediator.Send(new GetAllCountryQuery());
            return  NewResult(response);
        }



    }


     
}
