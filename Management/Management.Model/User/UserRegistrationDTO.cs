using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.User
{
    public class UserRegistrationDTO
    {
        [EmailAddress]
        [Required(ErrorMessage ="Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage ="User Name is required")]
        public string? UserName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Password should be of minimum 6 character and maximum 50 character.")]
        public string? Password { get; set; }
        public string? IpAddress { get; set; }
        public string? InvitationConfirmationURL { get; set; }

    }
}