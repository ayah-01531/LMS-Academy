using DalilAcademy.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static DalilAcademy.Core.DtOs.Auth;

namespace DalilAcademy_.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController (IAuthServices auth): ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginReponsetDTO>> Login(LoginRequestDTO dto) => Ok(await auth.LoginAsync(dto));
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterResponsetDTO>> Register(RegisterRequestDTO dto)
        {
            var created = await auth.RegisterAsync(dto);
            return StatusCode(StatusCodes.Status201Created, created);
        }
    }

}
