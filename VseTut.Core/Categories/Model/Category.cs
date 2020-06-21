using System;
using System.Collections.Generic;
using System.Text;

namespace VseTut.Core.Categories.Model
{
    public class Category
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Route { get; set; }
    }
}
