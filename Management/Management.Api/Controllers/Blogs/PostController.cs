using Management.Common;
using Management.Model.BlogModel;
using Management.Model.CommonModel;
using Management.Model.Data;
using Management.Services.Blogs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Management.Api.Controllers.Blogs
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostServices _postServices;
        public UserManager<ApplicationUser> _userManager;
        private readonly string requestTime = Utilities.GetRequestResponseTime();

        public PostController(IPostServices postServices, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _postServices = postServices;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        [ProducesResponseType(typeof(PostDTO), 201)]
        public async Task<IActionResult> CreatePost(PostDTO postDTO)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _postServices.CreatePost(postDTO, userId);

            if (result.success)
            {
                return Ok(new PayloadResponse<PostDTO>
                {
                    Message = result.Message,
                    Payload = result.data,
                    PayloadType = "Create Blog Post.",
                    RequestTime = requestTime,
                    ResponseTime = Utilities.GetRequestResponseTime(),
                    Success = true
                });
            }
            else
            {
                return new BadRequestObjectResult(new
                {
                    error = result.Message,
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    title = "One or more validation occurred.",
                    status = 400,
                    traceId = HttpContext != null? HttpContext.TraceIdentifier: null
                });

            }
        }

    }
}