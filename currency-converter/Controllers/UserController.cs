using currency_converter.Data.Implementations;
using currency_converter.Data.Interfaces;
using currency_converter.Data.Models.Dto.UserDtos;
using currency_converter.Data.Models.Enums;
using currency_converter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace currency_converter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : BaseController
    {
        //private readonly IConfiguration _config;
        private readonly string _passwordRegex;
        private readonly UserService _service;
        public UserController(UserService userService, IConfiguration config)
        {
            _service = userService;
            //_config = config;
            _passwordRegex = config["PasswordRegex"]!;
        }
        [HttpGet("admin/users")]
        public IActionResult GetAll()
        {
            if (!Admin()) return Forbid();
            return Ok(_service.GetAll());
        }
        [HttpGet("admin/users/{id}")]
        public IActionResult GetById(int id)
        {
            if (!Admin()) return Forbid();
            UserDto? user = _service.Get(id);
            return user is null ? NotFound(new Response()
            {
                Success = false,
                Error = new Error()
                {
                    Code = ErrorEnum.NotFound,
                    Message = $"User of id {id} not found"
                }
            }) : Ok(user);
        }
        [HttpGet("is-admin")]
        public IActionResult IsAdmin()
        {
            return Ok(Admin());
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create([FromBody] UserForCreationDto dto)
        {
            if (_service.Exists(dto.Email)) 
                return Conflict(new Response()
                    {
                        Success = false,
                        Error = new Error()
                        {
                            Code = ErrorEnum.InvalidPassword,
                            Message = "The provided email is already in use."
                        }
                    });
            if (!Regex.IsMatch(dto.Password, _passwordRegex))
                return BadRequest(new Response()
                    {
                        Success = false,
                        Error = new Error()
                        {
                            Code = ErrorEnum.InvalidPassword,
                            Message = "The provided password does not meet the required criteria. Please use a stronger password."
                        }
                    }); ;
            int newUserId = _service.Add(dto, false);
            return Created("/admin/users/", newUserId);
        }
        [HttpPut]
        public IActionResult Update([FromBody] UserForUpdateDto dto)
        {
            if (!Admin() && Email() != dto.Email) return Forbid();
            _service.Update(dto);
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] UserForDeletionDto dto)
        {
            if (!Admin() && Email() != dto.Email) return Forbid();
            if (!_service.Exists(dto.Email)) 
                return NotFound(new Response()
                    {
                        Success = false,
                        Error = new Error()
                        {
                            Code = ErrorEnum.NotFound,
                            Message = $"User of email '{dto.Email}' not found"
                        }
                    });
            _service.Delete(dto);
            return NoContent();
        }
    }
}
