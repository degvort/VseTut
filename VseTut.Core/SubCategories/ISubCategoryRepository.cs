using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VseTut.Core.SubCategories.Model;

namespace VseTut.Core.SubCategories
{
    public interface ISubCategoryRepository
    {
        Task CreateAsync(SubCategory subCategory);

        Task<SubCategory> GetByIdAsync(long id);

        void Update(SubCategory subCategory);

        void Delete(SubCategory subCategory);
    }
}
