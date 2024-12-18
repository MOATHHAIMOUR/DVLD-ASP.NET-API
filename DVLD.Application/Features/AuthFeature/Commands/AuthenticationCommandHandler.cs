using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites.Auth;
using MediatR;

namespace DVLD.Application.Features.AuthFeature.Commands
{
    public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, ApiResponse<AuthenticationResponse>>
    {
        IJWTAuthenticationWithRefreshTokenServices _jWTAuthenticationWithRefreshTokenServices;

        public AuthenticationCommandHandler(IJWTAuthenticationWithRefreshTokenServices jWTAuthenticationWithRefreshTokenServices)
        {
            _jWTAuthenticationWithRefreshTokenServices = jWTAuthenticationWithRefreshTokenServices;
        }

        public async Task<ApiResponse<AuthenticationResponse>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {

            var result = await _jWTAuthenticationWithRefreshTokenServices.AuthenticateUser(request.AuthenticationRequest.UserId, request.AuthenticationRequest.Password);

            if (!result.IsSuccess)
                return ApiResponseHandler.Unauthorized<AuthenticationResponse>(result.Error.Message);

            return ApiResponseHandler.Success(result.Value!);

        }
    }
}
