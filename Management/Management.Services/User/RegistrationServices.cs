using Management.Common.Configuration;
using Management.Model.CommonModel;
using Management.Model.Data;
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
    public class RegistrationServices : IRegistrationServices
    {
        private readonly ConnectionStringConfig _connectionStringConfig;
        private readonly UserManager<ApplicationUser> _userManager;
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        public RegistrationServices(ConnectionStringConfig connectionStringConfig, UserManager<ApplicationUser> userManager)
        {
            _userManager= userManager;
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