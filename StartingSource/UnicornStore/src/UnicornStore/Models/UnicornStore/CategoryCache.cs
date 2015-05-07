using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornStore.AspNet.Models.UnicornStore;

namespace UnicornStore.AspNet.Models.UnicornStore
{
    public class CategoryCache
    {
        private Lazy<Dictionary<int, Category>> byKey;
        private Lazy<Category[]> topLevel;

        public CategoryCache(UnicornStoreContext context)
        {
            byKey = new Lazy<Dictionary<int, Category>>(() => context.Categories.ToDictionary(c => c.CategoryId));
            topLevel = new Lazy<Category[]>(() => byKey.Value.Values.Where(c => c.ParentCategoryId == null).ToArray());
        }

        public Category FromKey(int categoryId)
        {
            return byKey.Value[categoryId];
        }

        public IEnumerable<Category> TopLevel()
        {
            return topLevel.Value;
        }

        public IEnumerable<Category> GetHierarchy(int categoryId)
        {
            var result = new List<Category>();
            var category = FromKey(categoryId);
            while(category != null)
            {
                result.Insert(0, category);
                category = category.ParentCategory;
            }

            return result;
        }

        public IEnumerable<int> GetThisAndChildIds(int categoryId)
        {
            return GetAllCategoryIdsIncludingChildren(new Category[] { FromKey(categoryId) });
        }

        private static IEnumerable<int> GetAllCategoryIdsIncludingChildren(IEnumerable<Category> categories)
        {
            return categories
                .Select(c => c.CategoryId)
                .Union(categories
                    .Where(c => c.Children != null)
                    .SelectMany(c => GetAllCategoryIdsIncludingChildren(c.Children)));
        }
    }
}
