using halalpizzabackend.Data;
using halalpizzabackend.Models;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.Linq;

public class UserDetailsService : IUserDetailsService
{
    private readonly ApplicationDbContext _dbContext;

    public UserDetailsService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public UserDetails Authenticate(string username, string password)
    {
        string hashedPassword = PasswordHasher.HashPassword(password);
        // Implement authentication logic here
        return _dbContext.UserDetails.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);
    }

    public UserDetails AddUser(UserDetails userDetails)
    {
        string hashedPassword = PasswordHasher.HashPassword(userDetails.Password);

        userDetails.Password = hashedPassword;
        _dbContext.UserDetails.Add(userDetails);
        _dbContext.SaveChanges();
        return userDetails;
    }

    public void DeleteUser(int userId)
    {
        // Implement user deletion logic here
        var user = _dbContext.UserDetails.Find(userId);
        if (user != null)
        {
            _dbContext.UserDetails.Remove(user);
            _dbContext.SaveChanges();
        }
      
    }
    
    public UserDetails EditUserWithNewPassword(int userId, UserDetailsEditModel userDetails)
    {
        // Implement user editing logic here
        var existingUser = _dbContext.UserDetails.Find(userId);
        if (existingUser != null)
        {
            existingUser.Name = userDetails.Name;
            existingUser.Avatar = userDetails.Avatar;
            existingUser.Email = userDetails.Email;
            existingUser.Username = userDetails.Username;

            if (!string.IsNullOrEmpty(userDetails.NewPassword))
            {
                string hashedPassword = PasswordHasher.HashPassword(userDetails.NewPassword);
                existingUser.Password = hashedPassword;
            }

            existingUser.Role = userDetails.Role;
            existingUser.CountryCode = userDetails.CountryCode;
            existingUser.MobileNumber = userDetails.MobileNumber;
            existingUser.Status = userDetails.Status;
            existingUser.BlockComments = userDetails.BlockComments;

            existingUser.SettingsJSON = userDetails.SettingsJSON;

         
                _dbContext.SaveChanges();
           
          
        }

        return existingUser;
    }

    public UserDetails EditMeWithNewPassword(int userId, UserDetailsEditModel userDetails)
    {
        // Implement user editing logic here
        var existingUser = _dbContext.UserDetails.Find(userId);
        if (existingUser != null)
        {
            existingUser.Name = userDetails.Name;
            existingUser.Avatar = userDetails.Avatar;
            existingUser.Email = userDetails.Email;
            existingUser.Username = userDetails.Username;

            if (!string.IsNullOrEmpty(userDetails.NewPassword))
            {
                string hashedPassword = PasswordHasher.HashPassword(userDetails.NewPassword);
                existingUser.Password = hashedPassword;
            }

           
            existingUser.CountryCode = userDetails.CountryCode;
            existingUser.MobileNumber = userDetails.MobileNumber;
          
          

    
            _dbContext.SaveChanges();
        }

        return existingUser;
    }
    public UserDetails EditUser(int userId, UserDetails userDetails)
    {
        // Implement user editing logic here
        var existingUser = _dbContext.UserDetails.Find(userId);
        if (existingUser != null)
        {
            existingUser.Name = userDetails.Name;
            existingUser.Avatar = userDetails.Avatar;
            existingUser.Email = userDetails.Email;
            existingUser.Username = userDetails.Username;

            string hashedPassword = PasswordHasher.HashPassword(userDetails.Password);
            existingUser.Password = hashedPassword;

            existingUser.Role = userDetails.Role;
            existingUser.CountryCode = userDetails.CountryCode;
            existingUser.MobileNumber = userDetails.MobileNumber;
            existingUser.Status = userDetails.Status;
            existingUser.BlockComments = userDetails.BlockComments;

            existingUser.SettingsJSON = userDetails.SettingsJSON;
            _dbContext.SaveChanges();
        }

        return existingUser;
    }

    public UserDetails GetUserById(int userId)
    {
        // Implement logic to retrieve a user by ID
        return _dbContext.UserDetails.Find(userId);
    }
    public IEnumerable<UserDetails> GetAllUsers()
    {
        // Implement logic to retrieve all users
        return _dbContext.UserDetails.OrderByDescending(s => s.ID).ToList();
    }
}
