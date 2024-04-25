using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace halalpizzabackend.Models
{
    public enum UserRole
    {
        User = 1,
        Admin = 2
    }

    public enum UserStatus
    {
        Active = 1,
        Blocked = 2,
        Inactive = 3
    }
    [Index(nameof(Username), IsUnique = true)]
    public class UserDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "longtext")] // MAX is used for large text
        public string? Avatar { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Email { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? CountryCode { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? MobileNumber { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string? BlockComments { get; set; }

        [Column(TypeName = "longtext")]
        public string? SettingsJSON { get; set; }
  
        [Required]
        public UserStatus Status { get; set; }
    }
}
