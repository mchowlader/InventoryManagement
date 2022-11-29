using Management.Api.SwaggerResponseExamples;
using Management.Common;
using Management.Model.CommonModel;
using Management.Model.Data;
using Management.Model.User;
using Management.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServices _authenticationServices;
        private readonly string requestTime = Utilities.GetRequestResponseTime();
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthenticationController(IAuthenticationServices authenticationServices, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _authenticationServices = authenticationServices;
        }

        [HttpPost]
        [Route("signup")]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(UserRegistrationDTO), 201)]
        public async Task<IActionResult> SignUp(UserRegistrationDTO userRegistrationDTO)
        {
            var result = await _authenticationServices.SignUp(userRegistrationDTO);

            return Ok(new PayloadResponse<UserRegistrationDTO>
            {
                Message = result.message,
                Payload = result.data,
                PayloadType = "SignUp",
                RequestTime = requestTime,
                Success = result.success
            });
        }

        [HttpPost]
        [Route("confirmsignup")]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(object), 2021)]
        public async Task<IActionResult> ConfirmSignUp(string email_confirmation_code)
        {
            var result = await _authenticationServices.ConfirmSignUp(email_confirmation_code);

            return Ok(new PayloadResponse<object>
            {
                Message = result.message,
                Payload = result.data,
                PayloadType = "ConfirmSignUp",
                RequestTime = requestTime,
                Success = result.success
            });
        }

        #region Private LoginService
        private async Task<IActionResult> ValidLoginResponse(ApplicationUser user, string deviceId)
        {

        }
        #endregion

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(LoginResponseViewModel), 201)]
        [ProducesResponseType(typeof(LoginUnauthorizeResponseViewModel), 401)]
        [ProducesResponseType(typeof(LoginNotFoundResponseExample), 404)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);

            #region Bad Login Attempts
            if (user == null
                ||
                user.IsRemoved
                ||
                !await _userManager.CheckPasswordAsync(user ?? new ApplicationUser(), login.Password)
               )
            {
                var response = new LoginUnauthorizeResponseViewModel
                {
                    FailedResponse = new FailedLoginResponse
                    {
                        Error = 401,
                        ErrorMessage = "Invalid Login Credentials."
                    }
                };

                return Ok(response);
            }

            var role = await _userManager.GetRolesAsync(user);

            if ((role.FirstOrDefault() == "User" || role.FirstOrDefault() == "Admin" || role.FirstOrDefault() == "CompanyAdmin")
                &&
                !user.EmailConfirmed
              )
            {
                var response = new FailedLoginResponse
                {
                    ErrorMessage = "Please confirm your email before loging in.",
                    Error = 501
                };

                return Ok(response);
            }
            #endregion

            #region Basic Login
            if (!user.TwoFactorEnabled || user.Email.IsNullOrEmpty() )
            {
                return await ValidLoginResponse(user, login.DeviceId); 
            }
            #endregion

            return Unauthorized();
        }
    }
}