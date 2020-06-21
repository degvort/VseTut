using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VseTut.Core.Users.Model
{
    [Table("Users")]
    public class User : IdentityUser
    {
    }
}
