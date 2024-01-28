using Microsoft.AspNetCore.Identity;

namespace Vidly7.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
