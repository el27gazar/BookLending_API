
using BookLending.Application.DTOsModel;
using BookLending.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLending.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;
        public AuthController(IAuth auth)
        {
            _auth = auth;
        }
            [HttpPost("Login")]
            public async Task<IActionResult> Login(DTOsLogin dTOsLogin)
        {
            var log = await _auth.Login(dTOsLogin);
            if (log.Token == null)
            {
                return Unauthorized();
            }
            return Ok(log);

        }
            [HttpPost("Register")]
            public async Task<IActionResult> Register(DTOsRegister dTOsRegister)
            {
                var reg = await _auth.Register(dTOsRegister);
                if (reg.Token == null)
                {
                    return BadRequest(reg);
                }
                return Ok(reg);
        }
    }
}
