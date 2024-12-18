﻿using DVLD.Application.Common.ResultPattern;
using DVLD.Domain.Entites.Auth;

namespace DVLD.Application.Services.IServices
{
    public interface IJWTAuthenticationWithRefreshTokenServices
    {
        public Task<Result<AuthenticationResponse>> AuthenticateUser(string userId, string password);

        //public Task<string> GenerateRefreshToken(string ipAddr, string userId);

    }
}