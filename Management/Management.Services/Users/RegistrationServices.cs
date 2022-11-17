using Management.Common;
using Management.Common.Configuration;
using Management.Model.CommonModel;
using Management.Model.Data;
using Management.Model.User;
using Management.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Services.Users
{
    public class RegistrationServices : IRegistrationServices
    {
        private readonly ConnectionStringConfig _connectionStringConfig;
        private readonly IUserServices _userServices;
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        public RegistrationServices(ConnectionStringConfig connectionStringConfig, IUserServices userServices)
        {
            _userServices = userServices;
            _connectionStringConfig= connectionStringConfig;
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
                        FirstName = "",
                        LastName = "",
                        UserRole = "",
                        VisibleEmailOption = false,
                        EmailVerificationLinkCode = email_verificaiotn_link_code,
                        EmailVerificationExpiry = Utilities.GetDate().AddDays(1)
                    };

                    var userCreation = await _userServices.CreateUserAsync(registerViewModel, adminUserId.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }
    }
    public interface IRegistrationServices
    {
        Task<ServiceResponse<UserRegistrationDTO>> SignUp(UserRegistrationDTO userRegistrationDTO);
    }
}