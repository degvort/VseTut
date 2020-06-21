using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VseTut.Core.Categories;
using VseTut.Core.Categories.Model;

namespace VseTut.EntityFrameworkCore.EntityFrameworkCore.Repositories.Users
{
    class CategoryRepository : ICategoryRepository
    {
        private readonly VseTutDbContext _context;

        public CategoryRepository(VseTutDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> GetByIdAsync(long id)
        {
            return await _context.Categories.Include(x => x.SubCategories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(x => x.SubCategories).ToListAsync();
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
    }
}
