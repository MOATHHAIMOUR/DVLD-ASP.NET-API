using DVLD.API.AppMetaData;
using DVLD.API.Controllers.Base;
using DVLD.Application.Common.ApiResponse;
using DVLD.Application.Features.AuthFeature.Commands.Authentication;
using DVLD.Application.Features.AuthFeature.Commands.RefreshToken;
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

            var userAgent = Request.Headers["User-Agent"].ToString();

            if (!IsMobileClient(userAgent))
                SetRefreshTokenInCookie(response.Data!.RefreshToken, response.Data.refreshTokenExpiration);

            return NewResult(response);

        }


        [HttpPost(Router.AuthRouting.RefreshToken)]
        public async Task<ActionResult<ApiResponse<AuthenticationResponse>>> RefreshToken()
        {

            string refreshToken = Request.Cookies["refreshToken"];

            var response = await _mediator.Send(new RefreshTokenCommand(refreshToken));

            var userAgent = Request.Headers["User-Agent"].ToString();

            if (response.Succeeded && !IsMobileClient(userAgent))
                SetRefreshTokenInCookie(response.Data!.RefreshToken, response.Data.refreshTokenExpiration);
            else ClearRefreshTokenCookie(); // Clear the cookie if the operation fails


            return NewResult(response);

        }


        private void ClearRefreshTokenCookie()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(-1), // Set the expiration to the past to clear the cookie
            };
            Response.Cookies.Append("refreshToken", string.Empty, cookieOptions);
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),

            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }


        private bool IsMobileClient(string userAgent)
        {
            // Simple check for mobile devices
            return userAgent.Contains("Android") || userAgent.Contains("iPhone") || userAgent.Contains("iPad");
        }

    }
}
