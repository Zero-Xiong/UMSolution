using System.Web.Http;
using UMServices;

namespace UMWebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/[controller]/[action]")]
    public class AccountController : ApiController
    {
        private readonly IUserService userService;

        public AccountController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpPost]
        public IHttpActionResult Login()
        {

            return Ok();
        }
    }
}
