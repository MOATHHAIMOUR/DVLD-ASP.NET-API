namespace DVLD.API.Controllers
{
    using DVLD.API.AppMetaData;
    using DVLD.API.Controllers.Base;
    using DVLD.Application.Common.ApiResponse;
    using DVLD.Application.DTO.Users;
    using DVLD.Application.Features.UserFeature.Command.AddUser;
    using DVLD.Application.Features.UserFeature.Command.DeleteUser;
    using DVLD.Application.Features.UserFeature.Command.UpdateUser;
    using DVLD.Application.Features.UserFeature.Quiers.GetUser;
    using DVLD.Application.Features.UserFeature.Quiers.GetUsers;
    using DVLD.Domain.DomainSearchParameters;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    public class UserController : AppController
    {
        public UserController(IMediator mediator) : base(mediator)
        { }

        [HttpGet(Router.UserRouting.GetUsers)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse<GetUserDTO>>> GetUsers([FromQuery] UsersSearchParameters UsersSearchParameters)
        {
            var response = await _mediator.Send(new GetUsersQuery(UsersSearchParameters));
            return NewResult(response);
        }

        [HttpGet(Router.UserRouting.GetUser)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<GetUserDTO>>> GetUser([FromQuery] int? UserId, [FromQuery] string? Username)
        {
            var response = await _mediator.Send(new GetUserQuery(UserId, Username));
            return NewResult(response);
        }

        [HttpDelete(Router.UserRouting.DeleteUser)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUser([FromRoute] int UserId)
        {
            var response = await _mediator.Send(new DeleteUserCommand(UserId));
            return NewResult(response);
        }

        [HttpPost(Router.UserRouting.AddUser)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddUser([FromBody] AddUserDTO addUserDto)
        {
            var response = await _mediator.Send(new AddUserCommand(addUserDto));
            return NewResult(response);
        }

        [HttpPut(Router.UserRouting.UpdateUser)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDto)
        {
            var response = await _mediator.Send(new UpdateUserCommand(updateUserDto));
            return NewResult(response);
        }
    }
}