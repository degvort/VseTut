using System;
using System.Collections.Generic;
using System.Text;
using VseTut.Core.Categories;
using VseTut.Core.Users;
using VseTut.Core.Users.Model;
using VseTut.EntityFrameworkCore.EntityFrameworkCore.Repositories.Users;

namespace VseTut.EntityFrameworkCore.EntityFrameworkCore.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VseTutDbContext _context;
        private UserRepository userRepository;
        private CategoryRepository categoryRespository;

        public UnitOfWork(VseTutDbContext context)
        {
            _context = context;
        }

        public IUserRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(_context);
                return userRepository;
            }
        }

        public ICategoryRepository Categories
        {
            get
            {
                if (categoryRespository == null)
                    categoryRespository = new CategoryRepository(_context);
                return categoryRespository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
