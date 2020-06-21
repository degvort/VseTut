using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VseTut.Web.Host.Models
{
    public class RefreshTokenResult
    {
        public string Id { get; set; }

        public string AccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public string RefreshToken { get; set; }

        public RefreshTokenResult(string accessToken, int expireInSeconds, string refreshToken, string userId)
        {
            Id = userId;
            AccessToken = accessToken;
            ExpireInSeconds = expireInSeconds;
            RefreshToken = refreshToken;
        }

        public RefreshTokenResult()
        {

        }
    }
}
