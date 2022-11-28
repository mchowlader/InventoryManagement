using Management.Model.User;
using Swashbuckle.AspNetCore.Filters;

namespace Management.Api.SwaggerResponseExamples
{
    public class LoginNotFoundResponseExample : IExamplesProvider<LoginUnauthorizeResponseViewModel>
    {
        public LoginUnauthorizeResponseViewModel GetExamples()
        {
            return new LoginUnauthorizeResponseViewModel
            {
                SuccessResponse = null,
                FailedResponse = new FailedLoginResponse
                {
                    Error = 404,
                    Token = null,
                }
            };
        }
    }
}