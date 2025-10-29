using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolProject.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }
        public string? Address { get; set; }
        public string? FullName { get; set; }
        public string? Country { get; set; }
        [EncryptColumn]
        public string? Code { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}
