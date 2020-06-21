using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using VseTut.Core.Auth;
using VseTut.Core.Auth.Enum;

namespace VseTut.Web.Core.Managers.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SECURITY_STAMP_SIZE = 32;

        public PasswordVerificationResult VerifyHashedPassword(string password, string passwordHash, string securityStamp)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentNullException(nameof(passwordHash));
            if (string.IsNullOrWhiteSpace(securityStamp)) throw new ArgumentNullException(nameof(securityStamp));

            var result = HashPassword(password, securityStamp);
            if (string.Compare(passwordHash, result, StringComparison.Ordinal) == 0)
            {
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }

        public string HashPassword(string password, string securityStamp)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(securityStamp)) throw new ArgumentNullException(nameof(securityStamp));

            var securityStampBytes = Encoding.UTF8.GetBytes(password + securityStamp);
            using (SHA512 hashProvider = new SHA512Managed())
            {
                var result = hashProvider.ComputeHash(securityStampBytes);
                return Convert.ToBase64String(result);
            }
        }

        public string CreateSecurityStamp()
        {
            var randBytes = new byte[SECURITY_STAMP_SIZE];
            var rand = new RNGCryptoServiceProvider();
            rand.GetBytes(randBytes);
            return Convert.ToBase64String(randBytes);
        }
    }
}
