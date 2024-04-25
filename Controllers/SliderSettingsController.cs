using halalpizzabackend.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/sliders")]
public class SliderSettingsController : ControllerBase
{
    private readonly ISliderSettingsService _sliderSettingsService;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public SliderSettingsController(ISliderSettingsService sliderSettingsService, IWebHostEnvironment hostingEnvironment)
    {
        _sliderSettingsService = sliderSettingsService;
        _hostingEnvironment = hostingEnvironment;
    }
   
    [HttpPost("add")]
    public IActionResult AddSlider([FromForm] SliderSettingsWithFile sliderSettingsWithFile)
    {
        try
        {
            // Extract slider settings from the form
            var sliderSettings = sliderSettingsWithFile.SliderSettings;

            // Check if the file is provided
            if (sliderSettingsWithFile.ImageFile != null && sliderSettingsWithFile.ImageFile.Length > 0)
            {
                // Process the file, save it, etc.
                // Example: Save to the wwwroot/images folder
                var uploadsFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + sliderSettingsWithFile.ImageFile.FileName;



                // Remove file extension

                uniqueFileName = Includes.filenameLimit(uniqueFileName);

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    sliderSettingsWithFile.ImageFile.CopyTo(fileStream);
                }

                // Set the image path in your SliderSettings object
                sliderSettings.Path = uniqueFileName;
            }

            // Add the slider with updated SliderSettings
            var addedSlider = _sliderSettingsService.AddSlider(sliderSettings);
            return Ok(addedSlider);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while adding the slider.");
            return BadRequest(new { message = "An error occurred while adding the slider." });
        }
  
        
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeleteSlider(int id)
    {
        try
        {
            _sliderSettingsService.DeleteSlider(id);
            return Ok(id);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest( new { message = "An error occurred while deleting the slider." });
        }
    }

    [HttpPut("edit/{id}")]
    public IActionResult EditSlider(int id, [FromForm] SliderSettingsWithFile sliderSettingsWithFile)
    {
        try
        {
            var sliderSettings = sliderSettingsWithFile.SliderSettings;

            // Check if the file is provided
            if (sliderSettingsWithFile.ImageFile != null && sliderSettingsWithFile.ImageFile.Length > 0)
            {
                // Process the file, save it, etc.
                // Example: Save to the wwwroot/images folder
                var uploadsFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + sliderSettingsWithFile.ImageFile.FileName;

                uniqueFileName = Includes.filenameLimit(uniqueFileName);

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    sliderSettingsWithFile.ImageFile.CopyTo(fileStream);
                }

                // Set the image path in your SliderSettings object
                sliderSettings.Path = uniqueFileName;
            }
            var editedSlider = _sliderSettingsService.EditSlider(id, sliderSettings);
            if (editedSlider == null)
                return NotFound();

            return Ok(editedSlider);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while editing the slider.");
            return BadRequest( new { message = "An error occurred while editing the slider." });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetSlider(int id)
    {
        try
        {
            var slider = _sliderSettingsService.GetSliderById(id);

            if (slider == null)
                return NotFound();

            return Ok(slider);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest( new { message = "An error occurred while fetching the slider." });
        }
    }

    [HttpGet("all/{filterEnabled}")]
    public IActionResult GetAllSliders(bool filterEnabled = false)
    {
        try
        {
            var sliders = _sliderSettingsService.GetAllSliders(filterEnabled);
            return Ok(sliders);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest( new { message = "An error occurred while fetching all sliders." });
        }
    }
}
