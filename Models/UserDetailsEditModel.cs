using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace halalpizzabackend.Models
{
   

    public class UserDetailsEditModel
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

    
        [Column(TypeName = "nvarchar(100)")]
        public string? NewPassword { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Password { get; set; }

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
