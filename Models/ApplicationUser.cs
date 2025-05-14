using Microsoft.AspNetCore.Identity;

namespace AgriEnergyConnect.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Store the user's role (e.g., "Farmer" or "Employee")
        public string Role { get; set; }
    }
}
