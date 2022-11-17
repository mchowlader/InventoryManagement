using Management.Common;
using Management.Model.CommonModel;
using Management.Model.Data;
using Management.Model.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Services.User
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string requestTime = Utilities.GetRequestResponseTime();
        public UserServices(UserManager<ApplicationUser> userManager)
        {
            _userManager= userManager;
        }
        public async Task<ServiceResponse<ApplicationUser>> CreateUserAsync(RegisterViewModel register, string identityUserId)
        {
            var user = new ApplicationUser();
            if(await _userManager.FindByNameAsync(register.UserName) == null && await _userManager.FindByEmailAsync(register.Email) == null)
            {
                user.CompanyId = register.CompanyId;
                user.UserName = register.UserName;
                user.Email = register.Email;
                user.FirstName= register.FirstName;
                user.LastName= register.LastName;
                user.IsRemoved = false;
                if(register.UserRole != "Admin")
                {
                    user.VisibleEmailOption = register.VisibleEmailOption;
                }
                user.EmailVerificationLinkCode= register.EmailVerificationLinkCode;
                user.EmailConfirmed = false;

                var result = await _userManager.CreateAsync(user);
                //if(!result.Succeeded)
                //{
                //    return new PayloadResponse<ApplicationUser>()
                //    {
                //        Success = false,
                //        Message = result.Errors.Select(x => x.Description.Replace("User name", "Username")).ToList(),
                //        Payload = null,
                //        PayloadType = "Create User",
                //        RequestTime = requestTime,
                //        ResponseTime = Utilities.GetRequestResponseTime()
                //    };
                //}

            }
            return default;

        }
    }
    public interface IUserServices
    {
    }
}