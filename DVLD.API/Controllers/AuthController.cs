using DVLD.API.AppMetaData;
using DVLD.API.Controllers.Base;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Features.AuthFeature.Commands;
using DVLD.Domain.Entites.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.API.Controllers
{
    public class AuthController : AppController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost(Router.AuthRouting.login)]
        public async Task<ActionResult<ApiResponse<AuthenticationResponse>>> UserAuthentication([FromBody] AuthenticationRequest authenticationRequest)
        {

            var response = await _mediator.Send(new AuthenticationCommand(authenticationRequest));

            return NewResult(response);

        }

    }
}
