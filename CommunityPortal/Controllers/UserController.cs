using Microsoft.AspNetCore.Mvc;

namespace CommunityPortal.Controllers
{

    [Route("api/[controller]")]
    public class UserController : Controller
    {

        // https://localhost:44373/api/User/GetMe

        [HttpGet("GetMe")]
        public string GetMe()
        {
            return ("Makrillen simmar i havet ");
        }
    }
}
