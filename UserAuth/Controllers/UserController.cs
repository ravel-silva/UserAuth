using Microsoft.AspNetCore.Mvc;
using UserAuth.Data;
using UserAuth.Data.Dtos;
using UserAuth.Services;

namespace UserAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public UserServices _services;
        public UserController(UserServices services)
        {
            _services = services;
        }
        [HttpPost("cadastro")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            return await _services.CreateUser(userDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto userDto)
        {
            await _services.Login(userDto);
            return Ok("Usuário autenticado!");
        }

    }
}
