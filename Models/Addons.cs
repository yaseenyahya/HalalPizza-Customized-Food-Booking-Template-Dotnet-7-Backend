using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace halalpizzabackend.Models
{
    public class Addons
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string ProductTitle { get; set; }

  
        [Column(TypeName = "nvarchar(300)")]
        public string? ProductImagePath { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }

        public decimal? ProductSalePrice { get; set; }


    }
}
