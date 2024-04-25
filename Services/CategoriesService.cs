using halalpizzabackend.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace halalpizzabackend.Models
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext _dbContext; // Replace YourDbContext with your actual DbContext

        public CategoriesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Categories AddCategory(Categories category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return category;
        }

        public List<Categories> GetAllCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public Categories GetCategoryById(int id)
        {
            return _dbContext.Categories.Find(id);
        }

        public Categories EditCategory(int id, Categories updatedCategory)
        {
            var existingCategory = _dbContext.Categories.Find(id);

            if (existingCategory != null)
            {
                existingCategory.Name = updatedCategory.Name;

                _dbContext.SaveChanges();
            }

            return existingCategory;
        }

        public void DeleteCategory(int id)
        {
            var category = _dbContext.Categories.Find(id);

            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
            }
        }
    }
}
