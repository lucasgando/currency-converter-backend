using currency_converter.Data.Models.Dto;
using currency_converter.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace currency_converter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserService _service;
        public UserController(UserService userService) { _service = userService; }
        [HttpGet("admin/users")]
        public IActionResult GetAll()
        {
            string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            if (userRole != "Admin") return Forbid();
            return Ok(_service.GetAll());
        }
        [HttpGet("admin/users/{id}")]
        public IActionResult GetById(int id)
        {
            string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            if (userRole != "Admin") return Forbid();
            UserDto? user = _service.GetById(id);
            if (user is not null) return Ok(user);
            return NotFound("User not found");
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create([FromBody] UserForCreationDto dto)
        {
            if (_service.Exists(dto.Email) || _service.Exists(dto.Username)) return Conflict("Email taken");
            _service.Add(dto, false);
            UserDto newUser = _service.GetByEmail(dto.Email)!;
            return Created(newUser.Id.ToString(), newUser);
        }
        [HttpPut]
        public IActionResult Update([FromBody] UserForUpdateDto dto)
        {
            string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            string email = User.Claims.First(claim => claim.Type.Contains("email")).Value;
            if (userRole != "Admin" && email != dto.Email) return Forbid();
            _service.Update(dto);
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] UserForDeletionDto dto)
        {
            string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            string email = User.Claims.First(claim => claim.Type.Contains("email")).Value;
            if (userRole != "Admin" && email != dto.Email) return Forbid();
            if (!_service.Exists(dto.Email)) return NotFound("User not found");
            _service.Delete(dto);
            return NoContent();
        }
    }
}
