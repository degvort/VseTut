using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VseTut.Core.Categories;
using VseTut.Core.Categories.Model;
using VseTut.EntityFrameworkCore.EntityFrameworkCore.Repositories;

namespace VseTut.Web.Core.Managers.Categories
{
    public class CategoryManager : ICategoryManager
    {
        private IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryManager(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        
        public async Task CreateCategoryAsync(CategoryDto input)
        {
            var category = _mapper.Map<Category>(input);
            await _uow.Categories.CreateAsync(category);
        }

        public async Task<CategoryDto> GetCategoryById(long id)
        {
            var category = await _uow.Categories.GetByIdAsync(id);
            if (category == null)
                throw new NullReferenceException($"Category with Id = '{id}', does not exist!");

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            var categories = await _uow.Categories.GetAllAsync();
            return _mapper.Map <List<CategoryDto>>(categories);
        }

        public async Task EditCategory(CategoryDto input)
        {
            var category = await _uow.Categories.GetByIdAsync((long)input.Id);

            if (category == null)
                throw new NullReferenceException("Current category does not exist!");

            if (!string.IsNullOrWhiteSpace(input.Title))
                category.Title = input.Title;
            if (!string.IsNullOrWhiteSpace(input.Description))
                category.Description = input.Description;
            if (!string.IsNullOrWhiteSpace(input.Image))
                category.Image = input.Image;
            if (!string.IsNullOrWhiteSpace(input.Route))
                category.Route = input.Route;

            _uow.Categories.Update(category);
        }

        public async Task DeleteCategory(long id)
        {
            var category = await _uow.Categories.GetByIdAsync(id);
            if(category == null) throw new Exception($"Category with id = {id} does not exist!");

            _uow.Categories.Delete(category);
        }
    }
}
