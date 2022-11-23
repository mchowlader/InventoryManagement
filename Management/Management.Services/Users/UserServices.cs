using Management.Common;
using Management.Common.Configuration;
using Management.Model.CommonModel;
using Management.Model.Data;
using Management.Model.DBModel;
using Management.Model.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace Management.Services.User
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MailConfiguration _mailConfiguration;
        private readonly ConnectionStringConfig _connectionStringConfig;

        private readonly string requestTime = Utilities.GetRequestResponseTime();
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        public UserServices(UserManager<ApplicationUser> userManager, IOptions<MailConfiguration> options, ConnectionStringConfig connectionStringConfig)
        {
            _userManager = userManager;
            _connectionStringConfig = connectionStringConfig;
            optionsBuilder.UseSqlServer(_connectionStringConfig.DefaultConnection);
            _mailConfiguration = options.Value;
        }
        public async Task<PayloadResponse<ApplicationUser>> CreateUserAsync(RegisterViewModel register, string identityUserId)
        {
            var user = new ApplicationUser();
            if (await _userManager.FindByNameAsync(register.UserName) == null && await _userManager.FindByEmailAsync(register.Email) == null)
            {
                user.CompanyId = register.CompanyId;
                user.UserName = register.UserName;
                user.Email = register.Email;
                user.FirstName = register.FirstName;
                user.LastName = register.LastName;
                user.IsRemoved = false;
                if (register.UserRole != "Admin")
                {
                    user.VisibleEmailOption = register.VisibleEmailOption;
                }
                user.EmailVerificationLinkCode = register.EmailVerificationLinkCode;
                user.EmailConfirmed = false;
                user.Country = register.Country;
                user.PhoneNumber = register.PhoneNumber;
                user.EmailVerificationExpiry = register.EmailVerificationExpiry;

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
                if (!result.Succeeded)
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
                    Success = true,
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
        public static int GetUserId(System.Security.Claims.ClaimsPrincipal user)
        {
            return (int)(user?.Identity?.Name?.ToInt32() ?? 0);
        }

        public async Task<bool> SendEmailConfirmationEmailAsync(ApplicationUser? user, string? invitationConfirmationURL)
        {
            try
            {
                string title = "Management Email Confirmation";
                var mailList = new List<string>()
            {
                user.Email
            };
                var mailBCCList = new List<string>();
                var mailCCList = new List<string>();
                var rasorModel = new
                {
                    Name = user.FirstName?.IsNullOrEmpty(user.UserName),
                    Url = invitationConfirmationURL + user.EmailVerificationLinkCode
                };

                var email = new MailMessage();
                email.From = new MailAddress(_mailConfiguration.Mail, _mailConfiguration.DisplayName);
                email.To.Add(user.Email);
                email.Subject = title;
                email.IsBodyHtml = true;
                email.Body = rasorModel.Url;
                using var smtp = new SmtpClient(_mailConfiguration.Host);
                smtp.Host = _mailConfiguration.Host;
                smtp.Port = _mailConfiguration.Port;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(_mailConfiguration.Mail, _mailConfiguration.Password);
                await smtp.SendMailAsync(email);
                smtp.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PayloadResponse<ApplicationUser>> DeleteUser(string email)
        {
            var applicationUser = await _userManager.FindByEmailAsync(email);
            _userManager.DeleteAsync(applicationUser);
            throw new NotImplementedException();
        }

        public async Task<bool> ConfirmEmail(ApplicationUser user, string email_confirmation_code, bool set_mfa = true)
        {
            try
            {
                if (!user.EmailConfirmed)
                {
                    var email_confirmation = user.EmailVerificationLinkCode == email_confirmation_code
                        && user.EmailVerificationExpiry >= Utilities.GetDate();
                    if (email_confirmation)
                    {
                        using (var context = new ApplicationDbContext(optionsBuilder.Options))
                        {
                            user = await context.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
                            user.EmailConfirmed = true;
                            user.EmailVerificationExpiry = null;
                            if (set_mfa) user.TwoFactorEnabled = true;
                            context.Users.Update(user);
                            await context.SaveChangesAsync();
                            return email_confirmation;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
    public interface IUserServices
    {
        Task<bool> ConfirmEmail(ApplicationUser user, string email_confirmation_code, bool set_mfa = true);
        Task<PayloadResponse<ApplicationUser>> CreateUserAsync(RegisterViewModel register, string identityUserId);
        Task<PayloadResponse<ApplicationUser>> DeleteUser(string email);
        Task<bool> SendEmailConfirmationEmailAsync(ApplicationUser? user, string? invitationConfirmationURL);
    }
}