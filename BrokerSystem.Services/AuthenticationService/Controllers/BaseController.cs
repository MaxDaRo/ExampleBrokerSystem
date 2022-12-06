using AuthenticationService.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Controller]
    public class BaseController : ControllerBase
    {
        public Account? Account => (Account?)HttpContext.Items["Account"];
    }
}
