﻿using System;
using System.Collections.Generic;
using System.Text;
using VseTut.Core.SubCategories.Model;

namespace VseTut.Core.Categories.Model
{
    public class Category
    {
        public Category()
        {
            SubCategories = new List<SubCategory>();
        }

        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Route { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
