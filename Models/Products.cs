using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace halalpizzabackend.Models
{
    public enum ProductType
    {
        Simple = 1,
        Deal = 2,
        Addon = 3
    }
    public enum ProductStatus
    {
        Enabled = 1,
        Disabled = 2
    }
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

      

        [Required]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Categories? Category { get; set; }

       

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string ProductTitle { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string ProductImagePath { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }

        public decimal? ProductSalePrice { get; set; }

        [Column(TypeName = "longtext")]
        public string? ProductDetails { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string? ProductDetailsImagePath { get; set; }


        [Column(TypeName = "nvarchar(100)")]
        public string? variationSelectionTitle { get; set; }

        [Column(TypeName = "longtext")]
        public string? variationSelectionItemsSerialized { get; set; }


        [Column(TypeName = "longtext")]
        public string? OtherSelectionSerialized { get; set; }

        [Column(TypeName = "nvarchar(400)")]
        public string? Note { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        public ProductStatus Enabled { get; set; }
    }
}
