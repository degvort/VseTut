using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using VseTut.Core.Users.Model;

namespace VseTut.Core.Auth
{
    public interface IJwtManager
    {
        bool IsRefreshTokenValid(string refreshToken, out ClaimsPrincipal principal);

        string CreateAccessToken(IEnumerable<Claim> claims);

        string CreateRefreshToken(IEnumerable<Claim> claims);

        string CreateToken(IEnumerable<Claim> claims, string expiration);

        IEnumerable<Claim> CreateJwtClaims(ClaimsIdentity identity, User user, TimeSpan? expiration = null);
    }
}
