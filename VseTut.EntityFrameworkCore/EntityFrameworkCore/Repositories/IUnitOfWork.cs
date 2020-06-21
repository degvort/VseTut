using System;
using System.Collections.Generic;
using System.Text;
using VseTut.Core.Categories;
using VseTut.Core.Users;
using VseTut.Core.Users.Model;

namespace VseTut.EntityFrameworkCore.EntityFrameworkCore.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository<User> Users { get; }

        ICategoryRepository Categories { get; }

        void Save();
    }
}
