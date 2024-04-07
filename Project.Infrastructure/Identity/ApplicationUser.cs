using Microsoft.AspNetCore.Identity;


namespace Project.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
