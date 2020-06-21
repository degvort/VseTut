using System;
using System.Collections.Generic;
using System.Text;

namespace VseTut.Core.Auth.Dto
{
    public class RegisterInput
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }
    }
}
