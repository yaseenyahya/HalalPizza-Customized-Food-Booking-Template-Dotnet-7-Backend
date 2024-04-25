using halalpizzabackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    [HttpPost("add")]
    public IActionResult AddCategory([FromBody] Categories category)
    {
        try
        {
            var addedCategory = _categoriesService.AddCategory(category);
            return CreatedAtAction(nameof(GetCategory), new { id = addedCategory.ID }, addedCategory);
        }
        catch (DbUpdateException ex)
        {
            var innerException = ex.InnerException as MySqlException;

            if (innerException != null && innerException.Number == 1062) // 2601 is the error code for unique constraint violation in SQL Server
            {

                // Provide a custom error message for duplicate Username
                return BadRequest(new { message = "The provided category is already in use. Please choose a different category." });
            }
            return BadRequest(new { message = "An error occurred while adding the category." });
        }
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeleteCategory(int id)
    {
        try
        {
            _categoriesService.DeleteCategory(id);
            return Ok(id);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest( new { message = "An error occurred while deleting the category." });
        }
    }

    [HttpPut("edit/{id}")]
    public IActionResult EditCategory(int id, [FromBody] Categories updatedCategory)
    {
        try
        {
            var editedCategory = _categoriesService.EditCategory(id, updatedCategory);
            if (editedCategory == null)
                return NotFound();

            return Ok(editedCategory);
        }
        catch (DbUpdateException ex)
        {
            var innerException = ex.InnerException as MySqlException;

            if (innerException != null && innerException.Number == 1062) // 2601 is the error code for unique constraint violation in SQL Server
            {

                // Provide a custom error message for duplicate Username
                return BadRequest(new { message = "The provided category is already in use. Please choose a different category." });
            }
            return BadRequest(new { message = "An error occurred while adding the category." });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetCategory(int id)
    {
        try
        {
            var category = _categoriesService.GetCategoryById(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest( new { message = "An error occurred while fetching the category." });
        }
    }

    [HttpGet("all")]
    public IActionResult GetAllCategories()
    {
        try
        {
            var categories = _categoriesService.GetAllCategories();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest( new { message = "An error occurred while fetching all categories." });
        }
    }
}
