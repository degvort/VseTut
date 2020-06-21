using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VseTut.Core.Categories.Model;

namespace VseTut.Core.Categories
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category category);

        Task<Category> GetByIdAsync(long id);

        Task<List<Category>> GetAllAsync();

        void Update(Category category);

        void Delete(Category category);
    }
}
