using DVLD.Application.Common.ApiResponse;
using DVLD.Domain.Entites.Auth;
using MediatR;

namespace DVLD.Application.Features.AuthFeature.Commands.Authentication
{
    public class AuthenticationCommand : IRequest<ApiResponse<AuthenticationResponse>>
    {
        public AuthenticationRequest AuthenticationRequest { set; get; }
        public AuthenticationCommand(AuthenticationRequest authenticationRequest)
        {
            AuthenticationRequest = authenticationRequest;
        }

    }
}
