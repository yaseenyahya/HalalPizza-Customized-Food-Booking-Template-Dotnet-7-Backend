using halalpizzabackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace halalpizzabackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<SliderSettings> SliderSettings { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Addons> Addons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Seed(); // Call the Seed method to insert data
        }
    }
   
}
