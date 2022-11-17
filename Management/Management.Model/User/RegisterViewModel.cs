using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.User
{
    public class RegisterViewModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set;}
        [Required]
        public string? Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password should be of minimum 6 character and maximum 50 character.")]
        public string? Password { get; set; }
        public Guid CompanyId { get; set; }
        public string? UserRole { get; set; }
        public string? IpAddress { get; set; }
        public bool VisibleEmailOption { get; set; }
        public string? EmailVerificationLinkCode { get; set; }
        public DateTime? EmailVerificationExpiry { get; set; }
    }
}