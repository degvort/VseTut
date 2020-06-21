using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using VseTut.Core.Auth;
using VseTut.Core.Users.Model;

namespace VseTut.Web.Core.Managers.Auth
{
    public class JwtManager : IJwtManager
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<JwtBearerOptions> _jwtOptions;
        private readonly IdentityOptions _identityOptions;
        private readonly UserManager<User> _userManager;

        public JwtManager(
            IConfiguration configuration,
            IOptions<JwtBearerOptions> jwtOptions,
            IdentityOptions identityOptions,
            UserManager<User> userManager)
        {
            _configuration = configuration;
            _jwtOptions = jwtOptions;
            _identityOptions = identityOptions;
            _userManager = userManager;
        }

        public bool IsRefreshTokenValid(string refreshToken, out ClaimsPrincipal principal)
        {
            principal = null;

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidAudience = _configuration["Jwt:Issuer"],
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                };

                foreach (var validator in _jwtOptions.Value.SecurityTokenValidators)
                {
                    if (validator.CanReadToken(refreshToken))
                    {
                        try
                        {
                            principal = validator.ValidateToken(refreshToken, validationParameters, out _);
                            return true;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        public string CreateAccessToken(IEnumerable<Claim> claims)
        {
            return CreateToken(claims, _configuration["Jwt:AccessTokenExpireDays"]);
        }

        public string CreateRefreshToken(IEnumerable<Claim> claims)
        {
            return CreateToken(claims, _configuration["Jwt:RefreshTokenEXpireDays"]);
        }

        public string CreateToken(IEnumerable<Claim> claims, string expiration)
        {
            var now = DateTime.UtcNow;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(expiration));

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                notBefore: now,
                signingCredentials: creds,
                expires: expires
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public IEnumerable<Claim> CreateJwtClaims(ClaimsIdentity identity, User user, TimeSpan? expiration = null)
        {
            var tokenValidityKey = Guid.NewGuid().ToString();
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == _identityOptions.ClaimsIdentity.UserIdClaimType);

            if (_identityOptions.ClaimsIdentity.UserIdClaimType != JwtRegisteredClaimNames.Sub)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value));
            }

            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(AppConst.TokenValidityKey, tokenValidityKey)
            });

            return claims;
        }
    }
}
