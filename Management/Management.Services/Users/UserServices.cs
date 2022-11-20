using Management.Common;
using Management.Model.CommonModel;
using Management.Model.Data;
using Management.Model.DBModel;
using Management.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        public UserServices(UserManager<ApplicationUser> userManager)
        {
            _userManager= userManager;
        }
        public async Task<PayloadResponse<ApplicationUser>> CreateUserAsync(RegisterViewModel register, string identityUserId)
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
                user.Country = register.Country;
                user.PhoneNumber = register.PhoneNumber;

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return new PayloadResponse<ApplicationUser>()
                    {
                        Success = false,
                        Message = result.Errors.Select(x => x.Description.Replace("User name", "Username")).ToList(),
                        Payload = null,
                        PayloadType = "Create User",
                        RequestTime = requestTime,
                        ResponseTime = Utilities.GetRequestResponseTime()
                    };
                }

                result = await _userManager.AddPasswordAsync(user, register.Password);
                if(!result.Succeeded)
                {
                    return new PayloadResponse<ApplicationUser>
                    {
                        Success = false,
                        Message = result.Errors.Select(x => x.Description.Replace("User Name", "Username")).ToList(),
                        Payload = null,
                        PayloadType = "Create User",
                        RequestTime = requestTime,
                        ResponseTime = Utilities.GetRequestResponseTime()

                    };
                }
                user.PasswordHash = null;

                using (var context = new ApplicationDbContext(optionsBuilder.Options))
                {
                    var action = await context.Actions.FirstOrDefaultAsync(x => x.Name == "Add user");
                    UserAuditLog userAuditLog = new UserAuditLog
                    {
                        ActionMoment = Utilities.GetDate(),
                        IpAddress = register.IpAddress,
                        ByUserId = identityUserId.ToInt32(),
                        AffectedUserId = user.Id,
                        ActionId = action.Id
                    };
                    await context.UserAuditLogs.AddAsync(userAuditLog);
                    await context.SaveChangesAsync();
                }

                return new PayloadResponse<ApplicationUser>
                {
                    Success = false,
                    Message = new List<string>() { "User creatd successfully." },
                    Payload = user,
                    PayloadType = "Create User",
                    RequestTime = requestTime,
                    ResponseTime = Utilities.GetRequestResponseTime()
                };

            }
            else
            {
                return new PayloadResponse<ApplicationUser>
                {
                    Success = false,
                    Message = new List<string>() { "User creation failed as username has already been taken." },
                    Payload = user,
                    PayloadType = "Create User",
                    RequestTime = requestTime,
                    ResponseTime = Utilities.GetRequestResponseTime()
                };
            }

        }
    }
    public interface IUserServices
    {
        public Task<PayloadResponse<ApplicationUser>> CreateUserAsync(RegisterViewModel register, string identityUserId);
    }
}