using System;
using System.Collections.Generic;
using System.Text;
using VseTut.Core.SubCategories.Dto;

namespace VseTut.Core.Categories
{
    public class CategoryDto
    {
        public long? Id { get; set; }

        public string Title { get; set; } = null;

        public string Description { get; set; } = null;

        public string Image { get; set; } = null;

        public string Route { get; set; } = null;

        public List<SubCategoryDto> SubCategories { get; set; }
    }
}
