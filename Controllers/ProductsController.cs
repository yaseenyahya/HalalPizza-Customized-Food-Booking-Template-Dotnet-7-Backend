using halalpizzabackend.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;
    private readonly IWebHostEnvironment _hostingEnvironment;
    public ProductsController(IProductsService productsService, IWebHostEnvironment hostingEnvironment)
    {
        _productsService = productsService;
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpPost("add")]
    public IActionResult AddProduct([FromForm] ProductsWithFile productsWithFile)
    {
        try
        {
            var products = productsWithFile.Products;

            var uploadsFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "images");
            if (productsWithFile.ProductImagePath != null && productsWithFile.ProductImagePath.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + productsWithFile.ProductImagePath.FileName;

                uniqueFileName = Includes.filenameLimit(uniqueFileName);

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productsWithFile.ProductImagePath.CopyTo(fileStream);
                }

                // Set the image path in your SliderSettings object
                products.ProductImagePath = uniqueFileName;
            }
            if (productsWithFile.ProductDetailsImagePath != null && productsWithFile.ProductDetailsImagePath.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + productsWithFile.ProductDetailsImagePath.FileName;

                uniqueFileName = Includes.filenameLimit(uniqueFileName);

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productsWithFile.ProductDetailsImagePath.CopyTo(fileStream);
                }

                // Set the image path in your SliderSettings object
                products.ProductDetailsImagePath = uniqueFileName;
            }
            var addedProduct = _productsService.AddProduct(products);
            return CreatedAtAction(nameof(GetProduct), new { id = addedProduct.ID }, addedProduct);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while adding the product.");
            return BadRequest( new { message = "An error occurred while adding the product." });
        }
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeleteProduct(int id)
    {
        try
        {
            _productsService.DeleteProduct(id);
            return Ok(true);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest( new { message = "An error occurred while deleting the product." });
        }
    }

    [HttpPut("edit/{id}")]
    public IActionResult EditProduct(int id, [FromForm] ProductsWithFile productsWithFile)
    {
        try
        {
            var products = productsWithFile.Products;

            var uploadsFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "images");
            if (productsWithFile.ProductImagePath != null && productsWithFile.ProductImagePath.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + productsWithFile.ProductImagePath.FileName;

                uniqueFileName = Includes.filenameLimit(uniqueFileName);

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productsWithFile.ProductImagePath.CopyTo(fileStream);
                }

                // Set the image path in your SliderSettings object
                products.ProductImagePath = uniqueFileName;
            }
            if (productsWithFile.ProductDetailsImagePath != null && productsWithFile.ProductDetailsImagePath.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + productsWithFile.ProductDetailsImagePath.FileName;

                uniqueFileName = Includes.filenameLimit(uniqueFileName);

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productsWithFile.ProductDetailsImagePath.CopyTo(fileStream);
                }

                // Set the image path in your SliderSettings object
                products.ProductDetailsImagePath = uniqueFileName;
            }

            var editedProduct = _productsService.EditProduct(id, products);
            if (editedProduct == null)
                return NotFound();

            return Ok(editedProduct);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while editing the product.");
            return BadRequest( new { message = "An error occurred while editing the product." });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        try
        {
            var product = _productsService.GetProductById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest( new { message = "An error occurred while fetching the product." });
        }
    }

    [HttpGet("all/{filterEnabled}/{filterDeal}")]
    public IActionResult GetAllProducts(bool filterEnabled = false, bool filterDeal = false)
    {
        try
        {
            var products = _productsService.GetAllProducts( filterEnabled,filterDeal);
            return Ok(products);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest( new { message = "An error occurred while fetching all products." });
        }
    }
}
