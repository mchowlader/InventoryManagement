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
        private readonly IAuthenticationServices _registrationServices;
        private readonly string requestTime = Utilities.GetRequestResponseTime();
        public AuthenticationController(IAuthenticationServices registrationServices)
        {
            _registrationServices = registrationServices;
        }

        [HttpPost]
        [Route("signup")]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(UserRegistrationDTO), 201)]
        public async Task<IActionResult> SignUp(UserRegistrationDTO userRegistrationDTO)
        {
            var result = await _registrationServices.SignUp(userRegistrationDTO);

            return Ok(new PayloadResponse<UserRegistrationDTO>
            {
                Message = result.message,
                Payload = result.data,
                PayloadType = "SignUp",
                RequestTime = requestTime,
                Success = result.success
            });
        }
    }
}