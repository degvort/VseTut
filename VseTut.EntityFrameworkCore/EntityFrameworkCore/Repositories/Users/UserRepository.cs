using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VseTut.Core.Users;
using VseTut.Core.Users.Model;

namespace VseTut.EntityFrameworkCore.EntityFrameworkCore.Repositories.Users
{
    class UserRepository : IRepository<User>, IUserRepository<User>
    {
        private readonly VseTutDbContext _context;

        public UserRepository(VseTutDbContext context)
        {
            _context = context;
        }

        public User GetUserByEmailAddress(string emailAddress)
        {
            return _context.Users.FirstOrDefault(u => u.Email == emailAddress);
        }

        public bool CheckUserName(string name)
        {
            var result = _context.Users.FirstOrDefault(x => x.UserName == name);
            if (result == null)
                return false;
            else
                return true;
        }

        public bool CheckUserEmailAddress(string emailAddress)
        {
            var result = _context.Users.FirstOrDefault(x => x.Email == emailAddress);
            if (result == null)
                return false;
            else
                return true;
        }
    }
}
