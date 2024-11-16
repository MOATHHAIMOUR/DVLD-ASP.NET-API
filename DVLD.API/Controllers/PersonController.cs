using DVLD.API.AppMetaData;
using DVLD.API.Controllers.Base;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.DTO.People;
using DVLD.Application.Features.PersonFeature.Command.AddPerson;
using DVLD.Application.Features.PersonFeature.Command.DeletePerson;
using DVLD.Application.Features.PersonFeature.Command.UpdatePerson;
using DVLD.Application.Features.PersonFeature.Queries.GetAllPersons;
using DVLD.Application.Features.PersonFeature.Queries.GetPerson;
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
        public async Task<ActionResult<ApiResponse<List<PeopleView>>>> GetPeople([FromQuery] PeopleSearchParamsDTO filters)
        {
            var response = await _mediator.Send(new GetAllPersonsQuery(filters));
            return  NewResult(response);
        }

        [HttpGet(Router.PersonRouting.GetPerson)]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        public async Task<ActionResult<ApiResponse<PersonDTO>>> GetPerson([FromQuery]int? PersonId, [FromQuery] string? NationalNo)
        {
            var response = await _mediator.Send(new GetPersonQuery(PersonId,NationalNo));

            return NewResult(response);
        }


        [HttpDelete(Router.PersonRouting.DeletePerson)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePerson([FromRoute] int PersonId)
        {
            var response = await _mediator.Send(new DeletePersonCommand(PersonId));

            return NewResult(response);
        }

        [HttpPost(Router.PersonRouting.AddPerson)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> AddPerson([FromForm] AddPersonDTO addPersonDto)
        {
            var response = await _mediator.Send(new AddPersonCommand(addPersonDto));

            return NewResult(response);
        }



        [HttpPut(Router.PersonRouting.UpdatePerson)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonDTO updatePersonDto)
        {
            var response = await _mediator.Send(new UpdatePersonCommand(updatePersonDto));

            return NewResult(response);
        }

    }



}
