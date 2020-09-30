using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Shared.Models.Input;

namespace User.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {

        }

        [HttpPost("")]
        public IActionResult CreateNewUser(CreateNewUserInputModel newUser)
        {
            return Ok();
        }
    }
}
