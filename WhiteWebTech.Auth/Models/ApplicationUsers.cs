using Microsoft.AspNetCore.Identity;

namespace WhiteWebTech.Auth.Models
{
    public class ApplicationUsers : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
