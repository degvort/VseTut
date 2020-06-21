using System;
using System.Collections.Generic;
using System.Text;
using VseTut.Core.Categories.Model;

namespace VseTut.Core.SubCategories.Model
{
    public class SubCategory
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Route { get; set; }

        public long? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
