using System;
using System.Collections.Generic;
using System.Text;

namespace VseTut.Core.SubCategories.Dto
{
    public class SubCategoryDto
    {
        public long? Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Route { get; set; }

        public long? CategoryId { get; set; }
    }
}
