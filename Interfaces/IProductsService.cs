using System.Collections.Generic;

namespace halalpizzabackend.Models
{
    public interface IProductsService
    {
        // Create a new product
        Products AddProduct(Products product);

        // Get all products
        List<Products> GetAllProducts(bool filterEnabled = false, bool filterDeal = false);

        // Get a specific product by ID
        Products GetProductById(int id);

        // Update a product by ID
        Products EditProduct(int id, Products updatedProduct);

        // Delete a product by ID
        void DeleteProduct(int id);
    }
}
