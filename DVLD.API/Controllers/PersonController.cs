using DVLD.API.AppMetaData;
using DVLD.API.Controllers.Base;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.People;
using DVLD.Application.Features.PersonFeature.Command.DeletePerson;
using DVLD.Application.Features.PersonFeature.Queries.GetAllPersons;
using DVLD.Domain.views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<ApiResponse<List<PeopleView>>>> GetPeople([FromQuery] PeopleSearchParams filters)
        {
            var response = await _mediator.Send(new GetAllPersonsQuery(filters));
            return  NewResult(response);
        }


        [HttpDelete(Router.PersonRouting.DeletePerson)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePerson([FromRoute] int PersonId)
        {
            var response = await _mediator.Send(new DeletePersonCommand(PersonId));

            return NewResult(response);
        }




    }



}
