using Microsoft.AspNetCore.Identity;

namespace OutOfOffice.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Login { get; set; } = string.Empty;
    }
}
