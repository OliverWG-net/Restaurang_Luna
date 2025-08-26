using Microsoft.AspNetCore.Mvc;
using Restaurang_luna.DTOs.Admin.Request;
using Restaurang_luna.DTOs.Admin.Response;
using Restaurang_luna.ServiceInterface.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurang_luna.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequestDto request, CancellationToken ct)
        {
            var LoginResponseDto = await _authService.Authenticate(request, ct);

            if (!LoginResponseDto.LoginSuccess)
            {
                return BadRequest("Invalid username or password");
            }

            if (LoginResponseDto == null)
            {
                return BadRequest("No user was found");
            }

            return Ok($"Welcome {LoginResponseDto.UserName}");
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
