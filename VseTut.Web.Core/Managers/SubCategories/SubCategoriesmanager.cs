using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VseTut.Core.SubCategories;
using VseTut.Core.SubCategories.Dto;
using VseTut.Core.SubCategories.Model;
using VseTut.EntityFrameworkCore.EntityFrameworkCore.Repositories;

namespace VseTut.Web.Core.Managers.SubCategories
{
    public class SubCategoriesManager : ISubCategoryManager
    {
        private IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SubCategoriesManager(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task CreateSubCategoryAsync(SubCategoryDto input)
        {
            var subCategory = _mapper.Map<SubCategory>(input);
            await _uow.SubCategories.CreateAsync(subCategory);
        }

        public async Task EditSubCategory(SubCategoryDto input)
        {
            var subCategory = await _uow.SubCategories.GetByIdAsync((long)input.Id);

            if (subCategory == null)
                throw new NullReferenceException("Current category does not exist!");

            if (!string.IsNullOrWhiteSpace(input.Title))
                subCategory.Title = input.Title;
            if (!string.IsNullOrWhiteSpace(input.Description))
                subCategory.Description = input.Description;
            if (!string.IsNullOrWhiteSpace(input.Image))
                subCategory.Image = input.Image;
            if (!string.IsNullOrWhiteSpace(input.Route))
                subCategory.Route = input.Route;

            _uow.SubCategories.Update(subCategory);
        }

        public async Task DeleteSubCategory(long id)
        {
            var subCategory = await _uow.SubCategories.GetByIdAsync(id);
            if (subCategory == null) throw new Exception($"SubCategory with id = {id} does not exist!");

            _uow.SubCategories.Delete(subCategory);
        }
    }
}
