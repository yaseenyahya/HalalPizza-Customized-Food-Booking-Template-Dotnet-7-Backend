using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace halalpizzabackend.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetails>().HasData(
                new UserDetails
                {
                    ID = 1,
                    Name = "John Doe",
                    Username = "admin",
                    Password = "2a97516c354b68848cdbd8f54a226a0a55b21ed138e207ad6c5cbb9c00aa5aea", // Replace with your hashed password
                    Role = UserRole.Admin,
                    Status = UserStatus.Active
                }
                // Add more seed data if needed
            );
        }
    }
}
