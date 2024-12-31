using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Services.IServices;
using DVLD.Domain.Entites.Auth;
using MediatR;

namespace DVLD.Application.Features.AuthFeature.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ApiResponse<AuthenticationResponse>>
    {

        private readonly IJWTAuthenticationWithRefreshTokenServices _jWTAuthenticationWithRefreshTokenServices;

        public RefreshTokenCommandHandler(IJWTAuthenticationWithRefreshTokenServices jWTAuthenticationWithRefreshTokenServices)
        {
            _jWTAuthenticationWithRefreshTokenServices = jWTAuthenticationWithRefreshTokenServices;
        }

        public async Task<ApiResponse<AuthenticationResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _jWTAuthenticationWithRefreshTokenServices.RefreshTokenAsync(request.RefreshToken);
            if (!result.IsSuccess)
                return ApiResponseHandler.BadRequest<AuthenticationResponse>([result.Error.Message]);

            return ApiResponseHandler.Success<AuthenticationResponse>(result.Value);
        }




    }
}


