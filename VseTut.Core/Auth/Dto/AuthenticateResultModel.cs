using System;
using System.Collections.Generic;
using System.Text;
using VseTut.Core.Users.Dto;

namespace VseTut.Core.Auth.Dto
{
    public class AuthenticateResultModel
    {
        public UserDto User { get; set; }

        public string AccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public string RefreshToken { get; set; }
    }
}
