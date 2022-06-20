using Microsoft.AspNetCore.Identity;

namespace MertBayraktar.Social.Network.Api.Entities.Data
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? Birthdate { get; set; }

    }
}
