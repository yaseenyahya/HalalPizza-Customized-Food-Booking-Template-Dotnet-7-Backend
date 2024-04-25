using halalpizzabackend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/addons")]
public class AddonsController : ControllerBase
{
    private readonly IAddonsService _addonsService;

    public AddonsController(IAddonsService addonsService)
    {
        _addonsService = addonsService;
    }

    [HttpPost("add")]
    public IActionResult AddAddon([FromForm] AddonsWithFile addonsWithFile)
    {
     
        try
        {
            // Extract slider settings from the form
            var addons = addonsWithFile.Addons;

            // Check if the file is provided
            if (addonsWithFile.ImageFile != null && addonsWithFile.ImageFile.Length > 0)
            {
                // Process the file, save it, etc.
                // Example: Save to the wwwroot/images folder
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + addonsWithFile.ImageFile.FileName;


                // Remove file extension

                uniqueFileName = Includes.filenameLimit(uniqueFileName);

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    addonsWithFile.ImageFile.CopyTo(fileStream);
                }

                // Set the image path in your SliderSettings object
                addons.ProductImagePath = uniqueFileName;
            }

            // Add the slider with updated SliderSettings
            var addedAddons = _addonsService.AddAddon(addons);
            return Ok(addedAddons);
     
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest(new { message = "An error occurred while adding the Addon." });
        }
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeleteADDON(int id)
    {
        try
        {
            _addonsService.DeleteAddon(id);
             return Ok(id);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest( new { message = "An error occurred while deleting the Addon." });
        }
    }

    [HttpPut("edit/{id}")]
    public IActionResult EditAddon(int id, [FromForm] AddonsWithFile addonsWithFile)
    {
        try
        {

            var addons = addonsWithFile.Addons;

            // Check if the file is provided
            if (addonsWithFile.ImageFile != null && addonsWithFile.ImageFile.Length > 0)
            {
                // Process the file, save it, etc.
                // Example: Save to the wwwroot/images folder
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + addonsWithFile.ImageFile.FileName;

                // Remove file extension

                uniqueFileName = Includes.filenameLimit(uniqueFileName);

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    addonsWithFile.ImageFile.CopyTo(fileStream);
                }

                // Set the image path in your SliderSettings object
                addons.ProductImagePath = uniqueFileName;
            }
            var editedAddon = _addonsService.EditAddon(id, addons);
            if (editedAddon == null)
                return NotFound();

            return Ok(editedAddon);
       
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest( new { message = "An error occurred while editing the addon." });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetAddon(int id)
    {
        try
        {
            var addon = _addonsService.GetAddonById(id);

            if (addon == null)
                return NotFound();

            return Ok(addon);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest( new { message = "An error occurred while fetching the addon." });
        }
    }

    [HttpGet("all")]
    public IActionResult GetAllAddons()
    {
        try
        {
            var addons = _addonsService.GetAllAddons();
            return Ok(addons);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest( new { message = "An error occurred while fetching all addons." });
        }
    }
}
