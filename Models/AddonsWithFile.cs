using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace halalpizzabackend.Models
{
    public class AddonsWithFile
    {
        public Addons Addons { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
