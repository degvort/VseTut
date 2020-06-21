using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using VseTut.Core.Users.Model;

namespace VseTut.Web.Core.Managers.Auth
{
    public class ClaimsIdentityFactory
    {
        public ClaimsIdentity Create(User user)
        {
            var claimsIdentity = new ClaimsIdentity();

            claimsIdentity.AddClaims(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.NormalizedUserName),
                new Claim(ClaimTypes.Email, user.Email)
            });

            return claimsIdentity;
        }
    }
}
