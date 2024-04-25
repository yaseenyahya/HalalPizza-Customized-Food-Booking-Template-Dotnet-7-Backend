using halalpizzabackend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/image")]
public class ViewImageController : ControllerBase
{
  
    [HttpGet("{imageName}")]
    public IActionResult GetImage(string imageName)
    {
        try
        {
            var imagePath = Path.Combine("images", imageName); // Update the path accordingly
            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            var fileExtension = Path.GetExtension(imagePath).ToLower();

            // Determine the content type based on the file extension
            string contentType;
            switch (fileExtension)
            {
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".gif":
                    contentType = "image/gif";
                    break;
                // Add more cases as needed for other image types
                default:
                    contentType = "application/octet-stream"; // Default to binary if type is unknown
                    break;
            }

            // Read the file content and return as File result

            return File(imageBytes, contentType);
        }catch(Exception ex)
        {
            return NotFound();
        }
    }
    
        
    

   
}
