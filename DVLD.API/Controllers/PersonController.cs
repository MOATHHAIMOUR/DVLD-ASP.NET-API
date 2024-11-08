using DVLD.API.AppMetaData;
using DVLD.API.Controllers.Base;
using DVLD.Application.Features.PersonFeature.Queries.GetAllPersons;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace DVLD.API.Controllers
{
    [ApiController]
    public class PersonController : AppController
    {
       
        public PersonController(IMediator mediator) : base(mediator)
        {
        }


        [HttpGet(Router.PersonRouting.GetPeople)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPeople()
        {
            var response = await _mediator.Send(new GetAllPersonsQuery());

            return  NewResult(response);
        }
    }
}
