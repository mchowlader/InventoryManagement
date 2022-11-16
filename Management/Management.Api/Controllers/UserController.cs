using Management.Model.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("signup")]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(UserRegistrationDTO), 201)]
        public async Task<IActionResult> SignUp(UserRegistrationDTO userRegistrationDTO)
        {
            return Ok(new PayloadResponse<UserRegistrationDTO>
            {

            });
        }
    }
}
