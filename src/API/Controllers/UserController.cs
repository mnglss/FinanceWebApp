using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FinanceWebApp.API.Controllers
{
    public class UserController : BaseApiController
    {
        [Authorize]
        [HttpGet()]
        public string[] GetUser()
        {
            return ["User1", "Uer2", "User3"];
        }
    }
}