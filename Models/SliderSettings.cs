using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace halalpizzabackend.Models
{
    public enum SliderStatus
    {
        Enabled = 1,
        Disabled = 2
    }
    public class SliderSettings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? Link { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string Path { get; set; }

        [Required]
        public SliderStatus Enabled { get; set; }
    }
}
