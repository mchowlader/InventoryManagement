using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Model.User
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? TwoFactorCode { get; set; }
        public int CurrencyFlag { get; set; }
        public int CurrencyStamp { get; set; }
        public string? DeviceId { get; set; }
    }
    public class LoginResponseViewModel
    {
        public SuccessfulLoginResponse SuccessReason { get; set; }
        public FailedLoginResponse FailedReason { get; set; }
        public bool TwoFactorEnable { get; set; }
        public bool TwoFactorCodeSend{ get; set; }
        public bool TwoFactorCodeValidate{ get; set; }

    }
    public class SuccessfulLoginResponse
    {

    }
    public class FailedLoginResponse
    {
        public int Error { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public string? DevieId { get; set; }
    }
    public class LoginUnauthorizeResponseViewModel
    {
        public SuccessfulLoginResponse SuccessResponse { get; set; }
        public FailedLoginResponse FailedResponse { get; set; }
    }
}
