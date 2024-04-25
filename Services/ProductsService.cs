using halalpizzabackend.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace halalpizzabackend.Models
{
    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext _dbContext; // Replace YourDbContext with your actual DbContext

        public ProductsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Products AddProduct(Products product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return product;
        }

        public List<Products> GetAllProducts(bool filterEnabled = false, bool filterDeal = false)
        {
            if(filterEnabled && filterDeal)
                return _dbContext.Products.Where(s => s.Enabled == ProductStatus.Enabled && s.ProductType == ProductType.Deal ).OrderByDescending(s => s.ID).ToList();
            else if (filterEnabled && !filterDeal)
                return _dbContext.Products.Where(s => s.Enabled == ProductStatus.Enabled && s.ProductType == ProductType.Simple).OrderByDescending(s => s.ID).ToList();
            else
            return _dbContext.Products.ToList();
        }

        public Products GetProductById(int id)
        {
            return _dbContext.Products.Find(id);
        }

        public Products EditProduct(int id, Products updatedProduct)
        {
            var existingProduct = _dbContext.Products.Find(id);

            if (existingProduct != null)
            {
                existingProduct.CategoryID = updatedProduct.CategoryID;
                existingProduct.ProductTitle = updatedProduct.ProductTitle;
                existingProduct.ProductImagePath = updatedProduct.ProductImagePath;
                existingProduct.ProductPrice = updatedProduct.ProductPrice;
                existingProduct.ProductSalePrice = updatedProduct.ProductSalePrice;
                existingProduct.ProductDetails = updatedProduct.ProductDetails;
                existingProduct.ProductDetailsImagePath = updatedProduct.ProductDetailsImagePath;
                existingProduct.variationSelectionTitle = updatedProduct.variationSelectionTitle;
                existingProduct.variationSelectionItemsSerialized = updatedProduct.variationSelectionItemsSerialized;
                existingProduct.OtherSelectionSerialized = updatedProduct.OtherSelectionSerialized;
               
               
                existingProduct.Note = updatedProduct.Note;
                existingProduct.ProductType = updatedProduct.ProductType;
                existingProduct.Enabled = updatedProduct.Enabled;

                _dbContext.SaveChanges();
            }

            return existingProduct;
        }

        public void DeleteProduct(int id)
        {
            var product = _dbContext.Products.Find(id);

            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }
    }
}
