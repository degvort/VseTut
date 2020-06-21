using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VseTut.Core.SubCategories.Dto;

namespace VseTut.Core.SubCategories
{
    public interface ISubCategoryManager
    {
        Task CreateSubCategoryAsync(SubCategoryDto input);

        Task EditSubCategory(SubCategoryDto input);

        Task DeleteSubCategory(long id);
    }
}
