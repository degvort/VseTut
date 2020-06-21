using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VseTut.Core.Categories
{
    public interface ICategoryManager
    {
        Task CreateCategoryAsync(CategoryDto input);

        Task<CategoryDto> GetCategoryById(long id);

        Task<List<CategoryDto>> GetCategories();

        Task EditCategory(CategoryDto input);

        Task DeleteCategory(long id);
    }
}
