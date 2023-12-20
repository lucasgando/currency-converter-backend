using currency_converter.Data.Models.Dto;
using currency_converter.Data.Models.Dto.UserDtos;
using currency_converter.Helpers;
using currency_converter.Services;
using Microsoft.AspNetCore.Mvc;

namespace currency_converter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        public readonly IConfiguration _config;
        public readonly UserService _service;
        public AuthController(IConfiguration config, UserService service)
        {
            _config = config;
            _service = service;
        }
        [HttpPost]
        public IActionResult Authenticate([FromBody] CredentialsDto dto)
        {
            UserDto? user = _service.Get(dto.Email);
            if (user is null) return Unauthorized();
            if (!_service.Authenticate(dto.Email, dto.Password)) return Unauthorized();
            return Ok(AuthJwtGenerator.GenerateJWT(user, _config, DateTime.UtcNow.AddDays(1)));
        }
    }
}
