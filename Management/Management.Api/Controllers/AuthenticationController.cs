using Management.Common;
using Management.Model.CommonModel;
using Management.Model.User;
using Management.Services.Users;
using Microsoft.AspNetCore.Http;
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
        public AuthenticationController(IAuthenticationServices authenticationServices)
        {
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
    }
}