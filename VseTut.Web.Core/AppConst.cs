using System;
using System.Collections.Generic;
using System.Text;

namespace VseTut.Web.Core
{
    public class AppConst
    {
        public const string TokenValidityKey = "vse_tut_jwt_token_key";

        public static TimeSpan AccessTokenExpiration = TimeSpan.FromDays(1);
        public static TimeSpan RefreshTokenExpiration = TimeSpan.FromDays(365);
    }
}
