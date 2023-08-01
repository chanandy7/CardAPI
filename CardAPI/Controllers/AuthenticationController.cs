using Cards.Core;
using Cards.Core.customExceptions;
using Cards.Core.CustomExceptions;
using Cards.DB;
using Microsoft.AspNetCore.Mvc;

namespace CardAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        //endpoints
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(User user)
        {
            try {
                var result = await _userService.SignUp(user);
                return Created("", result);

            }
            catch(UsernameAlreadyExistsException e) {
                return StatusCode(409, e.Message);
            }


        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin(User user)
        {
            try
            {
                var result = await _userService.SignIn(user);
                return Ok(result);
            }
            catch(InvalidUsernamePasswordException e)
            {
                return StatusCode(401, e.Message);
            }
        }
    }
}
