using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Management.Model.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required(ErrorMessage ="First name is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string? LastName { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsRemoved { get; set; }
        [Required(ErrorMessage = "Country name is required")]
        public string? Country { get; set; }
        public string? EmailVerificationLinkCode { get; set; }
        public bool VisibleEmailOption { get; set; }
        public DateTime? EmailVerificationExpiry { get; set; }
    }
    public class Role : IdentityRole<Guid>
    {
        public Role() 
            : base()
        {

        }
        public Role(string roleName) 
            : base(roleName)
        {

        }
    }
    public class UserClaim 
        : IdentityUserClaim<Guid>
    {

    }
    public class UserRole 
        : IdentityUserRole<Guid>
    {

    }
    public class UserLogin 
        : IdentityUserLogin<Guid>
    {

    }
    public class Roleclaim 
        : IdentityRoleClaim<Guid>
    {

    }
    public class UserToken 
        : IdentityUserToken<Guid>
    {

    }
}