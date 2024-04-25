using halalpizzabackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/users")]
public class UserDetailsController : ControllerBase
{
    private readonly IUserDetailsService _userService;

    public UserDetailsController(IUserDetailsService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel loginModel)
    {
        try { 
        var user = _userService.Authenticate(loginModel.Username, loginModel.Password);

        if (user == null || user.Role != UserRole.Admin)
            return Unauthorized(new { message = "Invalid credentials" });

        if (user.Status != UserStatus.Active)
        {
            // Handle inactive, blocked, or any other non-active status
            string errorMessage = user.Status == UserStatus.Blocked ? "User is blocked" : "User is inactive";
            return BadRequest(new { message = errorMessage });
        }

        // Create a session or token (based on your authentication mechanism)
        // For simplicity, using a cookie to simulate a session


        return Ok(user);
    }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest(new { message = "An error occurred while logging user." });
        }
    }

    [HttpPost("add")]
    public IActionResult AddUser([FromBody] UserDetails userDetails)
    {
        try
        {
            var addedUser = _userService.AddUser(userDetails);

            return CreatedAtAction(nameof(GetUser), new { id = addedUser.ID }, addedUser);

        }
        catch (DbUpdateException ex)
        {
            var innerException = ex.InnerException as MySqlException;

            if (innerException != null && innerException.Number == 1062) // 2601 is the error code for unique constraint violation in SQL Server
            {
               
                // Provide a custom error message for duplicate Username
                return BadRequest(new { message = "The provided username is already in use. Please choose a different username." });
            }
            return BadRequest(new { message = "An error occurred while adding the user." });
        }
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeleteUser(int id)
    {
        try
        {
            _userService.DeleteUser(id);
            return Ok(id);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest(new { message = "An error occurred while deleting the user." });
        }
        

        
    }

    [HttpPut("editme/{id}")]
    public IActionResult EditMe(int id, [FromBody] UserDetailsEditModel userDetails)
    {
        try
        {
            var user = _userService.Authenticate(userDetails.Username, userDetails.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid password" });

            var updatedUser = _userService.EditMeWithNewPassword(id, userDetails);

            return Ok(updatedUser);
        }
        catch (DbUpdateException ex)
        {
            var innerException = ex.InnerException as MySqlException;

            if (innerException != null && innerException.Number == 1062) // 2601 is the error code for unique constraint violation in SQL Server
            {
                // Provide a custom error message for duplicate Username
                // Provide a custom error message for duplicate Username
                return BadRequest(new { message = "The provided username is already in use. Please choose a different username." });
            }
            return BadRequest(new { message = "An error occurred while editing the user." });
        }
    }
    [HttpPut("editwithnewpassword/{id}")]
    public IActionResult EditUserWithNewPassword(int id, [FromBody] UserDetailsEditModel UserDetailsEditModel)
    {
        try
        {
            var updatedUser = _userService.EditUserWithNewPassword(id, UserDetailsEditModel);

            return Ok(updatedUser);
        }
        catch (DbUpdateException ex)
        {
            var innerException = ex.InnerException as MySqlException;

            if (innerException != null && innerException.Number == 1062) // 2601 is the error code for unique constraint violation in SQL Server
            {
                // Provide a custom error message for duplicate Username
                return BadRequest(new { message = "The provided username is already in use. Please choose a different username." });
            }
            return BadRequest(new { message = "An error occurred while editing the user." });
        }

    }
    [HttpPut("edit/{id}")]
    public IActionResult EditUser(int id, [FromBody] UserDetailsEditModel userDetails)
    {
        try
        {
            var updatedUser = _userService.EditUserWithNewPassword(id, userDetails);

            return Ok(updatedUser);
        }
        catch (DbUpdateException ex)
        {
            var innerException = ex.InnerException as MySqlException;

            if (innerException != null && innerException.Number == 1062) // 2601 is the error code for unique constraint violation in SQL Server
            {
                // Provide a custom error message for duplicate Username
                return BadRequest(new { message = "The provided username is already in use. Please choose a different username." });
            }
            return BadRequest(new { message = "An error occurred while editing the user." });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        try
        {
            var user = _userService.GetUserById(id);

        if (user == null)
            return NotFound();

        return Ok(user);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest(new { message = "An error occurred while fetching the user." });
        }
    }

    [HttpGet("all")]
    public IActionResult GetAllUsers()
    {
        try
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            // Handle exceptions, log errors, etc.
            return BadRequest(new { message = "An error occurred while fetching all users." });
        }
       
    }
}
