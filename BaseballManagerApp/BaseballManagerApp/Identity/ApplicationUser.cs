using Microsoft.AspNetCore.Identity;

namespace BaseballManagerApp.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
