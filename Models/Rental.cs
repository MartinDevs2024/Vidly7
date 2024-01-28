using System.ComponentModel.DataAnnotations;

namespace Vidly7.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public Customer Customer { get; set; }

        public Movie Movie { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime DateReturned { get; set; }

    }
}
