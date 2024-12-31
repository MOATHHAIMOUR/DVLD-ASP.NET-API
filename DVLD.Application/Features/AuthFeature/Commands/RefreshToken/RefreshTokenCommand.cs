using DVLD.Application.Common.ApiResponse;
using DVLD.Domain.Entites.Auth;
using MediatR;

namespace DVLD.Application.Features.AuthFeature.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<ApiResponse<AuthenticationResponse>>
    {
        public string RefreshToken { get; set; }

        public RefreshTokenCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}

