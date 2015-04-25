using System;
using System.Collections.Generic;

namespace UnicornStore.AspNet.Models.UnicornStore
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string DisplayName { get; set; }

        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public List<Category> Children { get; set; }

        public List<Product> Products { get; set; }
    }
}