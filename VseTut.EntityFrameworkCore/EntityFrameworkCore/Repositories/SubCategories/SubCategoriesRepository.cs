using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VseTut.Core.SubCategories;
using VseTut.Core.SubCategories.Model;

namespace VseTut.EntityFrameworkCore.EntityFrameworkCore.Repositories.SubCategories
{
    public class SubCategoriesRepository : ISubCategoryRepository
    {
        private readonly VseTutDbContext _context;

        public SubCategoriesRepository(VseTutDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(SubCategory subCategory)
        {
            await _context.SubCategories.AddAsync(subCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<SubCategory> GetByIdAsync(long id)
        {
            return await _context.SubCategories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(SubCategory subCategory)
        {
            _context.SubCategories.Update(subCategory);
            _context.SaveChanges();
        }

        public void Delete(SubCategory subCategory)
        {
            _context.SubCategories.Remove(subCategory);
            _context.SaveChanges();
        }
    }
}
