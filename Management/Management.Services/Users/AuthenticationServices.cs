using Management.Common;
using Management.Common.Configuration;
using Management.Model.CommonModel;
using Management.Model.Data;
using Management.Model.User;
using Management.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Management.Services.Users
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly ConnectionStringConfig _connectionStringConfig;
        private readonly IUserServices _userServices;
        private readonly UserManager<ApplicationUser> _userManager;
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        public AuthenticationServices(ConnectionStringConfig connectionStringConfig, IUserServices userServices)
        {
            _connectionStringConfig = connectionStringConfig;
            optionsBuilder.UseSqlServer(_connectionStringConfig.DefaultConnection);
            _userServices = userServices;
        }

        public async Task<ServiceResponse<object>> ConfirmSignUp(string email_confirmation_code)
        {
            using(var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                var user = await context.Users
                   .Where(x => x.EmailVerificationLinkCode == email_confirmation_code
                   && x.EmailVerificationExpiry >= Utilities.GetDate())
                   .FirstOrDefaultAsync();
                if (user == null)
                    return ServiceResponse<object>.Error("Account activation link has been expired");
                if (user.EmailConfirmed)
                    return ServiceResponse<object>.Success("Your email is already confirmed. Please sign in with your username and password to continue.");

                var email_confirmation = await _userServices.ConfirmEmail(user, email_confirmation_code, true);
                if (!email_confirmation)
                    return ServiceResponse<object>.Error("Account activation link has been expired");

                return ServiceResponse<object>.Success("Your email has been confirmed. Please sign in with your username and password to set up your account.");
            }
        }

        public async Task<ServiceResponse<UserRegistrationDTO>> SignUp(UserRegistrationDTO userRegistrationDTO)
        {
            try
            {
                using (var context = new ApplicationDbContext(optionsBuilder.Options))
                {
                    if(await context.Users.Where(x => x.Email == userRegistrationDTO.Email).CountAsync() > 0)
                    {
                        return ServiceResponse<UserRegistrationDTO>.Error("Email is already taken.");
                    }
                    var adminUserId = await context.Users.Where(x => x.UserName == "AutomationUser").Select(x => x.Id).FirstOrDefaultAsync();

                    var email_verificaiotn_link_code = Utilities.GeerateRandomCodeStringByBiteSize(25).Take(25);
                    Generate_Email_Verifiction_Link_code:
                    if (await context.Users.AnyAsync(x => x.EmailVerificationLinkCode == email_verificaiotn_link_code))
                    {
                        goto Generate_Email_Verifiction_Link_code;
                    }

                    RegisterViewModel registerViewModel = new RegisterViewModel()
                    {
                        Email = userRegistrationDTO.Email,
                        IpAddress = userRegistrationDTO.IpAddress,
                        Password = userRegistrationDTO.Password,
                        UserName = userRegistrationDTO.UserName,
                        Country = userRegistrationDTO.Country,
                        FirstName = "",
                        LastName = "",
                        UserRole = _EnumObject.Role.SuperAdmin.ToString(),
                        PhoneNumber = userRegistrationDTO.PhoneNumber,
                        VisibleEmailOption = false,
                        EmailVerificationLinkCode = email_verificaiotn_link_code,
                        EmailVerificationExpiry = Utilities.GetDate().AddDays(1)
                    };

                    var userCreation = await _userServices.CreateUserAsync(registerViewModel, adminUserId.ToString());

                    if(userCreation.Success) 
                    { 
                        var user = await context.Users.Where(x => x.Id == userCreation.Payload.Id).FirstOrDefaultAsync();
                        var sendEmail = await _userServices.SendEmailConfirmationEmailAsync(user, userRegistrationDTO.InvitationConfirmationURL);

                        if(sendEmail)
                        {
                            return ServiceResponse<UserRegistrationDTO>.Success("Please check your email inbox for verification email");
                        }
                        else
                        {
                            await _userServices.DeleteUser(userRegistrationDTO.Email);
                            return ServiceResponse<UserRegistrationDTO>.Error("There is a problem with user sign up. Please try again.");
                        }
                    }
                    else
                    {
                        return ServiceResponse<UserRegistrationDTO>.Error("Sign up failed : " + userCreation.Message.ToCommaSeparatedString());
                    }
                }
            }
            catch (Exception ex)
            {

                await _userServices.DeleteUser(userRegistrationDTO.Email);
                return ServiceResponse<UserRegistrationDTO>.Error("Sign up failed : " + ex.Message);
            }
        }
    }
    public interface IAuthenticationServices
    {
        Task<ServiceResponse<object>> ConfirmSignUp(string email_confirmation_code);
        Task<ServiceResponse<UserRegistrationDTO>> SignUp(UserRegistrationDTO userRegistrationDTO);
    }
}