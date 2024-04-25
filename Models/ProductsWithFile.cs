using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace halalpizzabackend.Models
{
    public class ProductsWithFile
    {
        public Products Products { get; set; }
        public IFormFile ProductImagePath { get; set; }

        public IFormFile ProductDetailsImagePath { get; set; }

    }
}
