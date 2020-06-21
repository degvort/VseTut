using System;
using System.Collections.Generic;
using System.Text;
using VseTut.Core.Auth.Enum;

namespace VseTut.Core.Auth
{
    public interface IPasswordHasher
    {
        string HashPassword(string password, string securityStamp);

        PasswordVerificationResult VerifyHashedPassword(string password, string passwordHash, string securityStamp);

        string CreateSecurityStamp();
    }
}
