using System.ComponentModel.DataAnnotations;

namespace Vidly7.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
