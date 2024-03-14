using Microsoft.AspNetCore.Identity;

namespace BlazorApp4.Data
{
    
    public class ApplicationUser : IdentityUser
    {

        public byte[]? aesKey { get; set; }
        public byte[]? iv { get; set; }
        public string Role { get; set; } = "unprivileged";

    }

}
