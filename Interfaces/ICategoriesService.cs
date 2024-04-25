using System.Collections.Generic;

namespace halalpizzabackend.Models
{
    public interface ICategoriesService
    {
        // Create a new category
        Categories AddCategory(Categories category);

        // Get all categories
        List<Categories> GetAllCategories();

        // Get a specific category by ID
        Categories GetCategoryById(int id);

        // Update a category by ID
        Categories EditCategory(int id, Categories updatedCategory);

        // Delete a category by ID
        void DeleteCategory(int id);
    }
}
