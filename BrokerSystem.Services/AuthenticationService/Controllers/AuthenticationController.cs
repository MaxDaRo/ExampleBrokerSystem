using AuthenticationService.BLL.Contracts;
using AuthenticationService.Common.Constants;
using AuthenticationService.Data.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthLogic _authLogic;

        public AuthenticationController(
            IAuthLogic authLogic)
        {
            _authLogic = authLogic;

        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> Authenticate(AuthenticationRequest authenticationRequest)
        {
            var response = await _authLogic.Authenticate(authenticationRequest);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            Response.Cookies.Append(AuthConsants.JwtCookieKey, response.AccessToken);

            return Ok(response);
        }
    }
}
