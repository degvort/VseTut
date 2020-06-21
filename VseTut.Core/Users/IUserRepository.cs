using System;
using System.Collections.Generic;
using System.Text;
using VseTut.Core.Users.Model;

namespace VseTut.Core.Users
{
    public interface IUserRepository<TUser> where TUser : class
    {
        User GetUserByEmailAddress(string emailAddress);

        bool CheckUserName(string name);

        bool CheckUserEmailAddress(string emailAddress);
    }
}
