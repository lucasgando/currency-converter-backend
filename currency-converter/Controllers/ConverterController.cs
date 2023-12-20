using currency_converter.Data.Implementations;
using currency_converter.Data.Interfaces;
using currency_converter.Data.Models.Dto.ConversionDtos;
using currency_converter.Data.Models.Enums;
using currency_converter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace currency_converter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ConverterController : BaseController
    {
        private readonly ConverterService _service;
        public ConverterController(ConverterService service) { _service = service; }
        [HttpGet("admin/conversions")]
        public IActionResult GetAll()
        {
            if (!Admin()) return Forbid();
            return Ok(_service.AdminGetAll());
        }
        [HttpGet("admin/conversions/{id}")]
        public IActionResult Get(int id)
        {
            if (!Admin()) return Forbid();
            if (!_service.Exists(id)) 
                return NotFound(new Response()
                    {
                        Success = false,
                        Error = new Error()
                        {
                            Code = ErrorEnum.NotFound,
                            Message = $"Conversion of id {id} not found."
                        }
                    });
            return Ok(_service.AdminGet(id));
        }
        [HttpGet("admin/conversions/user/{id}")]
        public IActionResult GetByUserId(int id)
        {
            if (!Admin()) return Forbid();
            return Ok(_service.GetAll(id));
        }
        [HttpGet("can-convert")]
        public IActionResult CanConvert()
        {
            return Ok(_service.CanConvert(UserId()));
        }
        [HttpGet("conversions")]
        public IActionResult GetByUser()
        {
            return Ok(_service.GetAll(UserId()));
        }
        [HttpGet("remaining")]
        public IActionResult RemainingConversions()
        {
            return Ok(_service.RemainingConversions(UserId()));
        }
        // maybe user get by id
        [HttpPost]
        public IActionResult Convert([FromBody] ConversionRequestDto dto)
        {
            if (dto.Amount <= 0) 
                return BadRequest(new Response()
                {
                    Success = false,
                    Error = new Error()
                    {
                        Code = ErrorEnum.InvalidOperation,
                        Message = "Only positive amounts accepted."
                    }
                });
            if (!_service.CanConvert(UserId())) 
                return BadRequest(new Response()
                {
                    Success = false,
                    Error = new Error()
                    {
                        Code = ErrorEnum.ConversionLimitExceeded,
                        Message = "You have exceeded your conversion attempt limits. Please upgrade your subscription."
                    }
                });
            if (!_service.ValidCurrency(dto.FromCurrencyId) || !_service.ValidCurrency(dto.ToCurrencyId))
                return NotFound(new Response()
                {
                    Success = false,
                    Error = new Error()
                    {
                        Code = ErrorEnum.InvalidOperation,
                        Message = "Currencies ids not found."
                    }
                });
            return Ok(_service.Convert(dto, UserId()));
        }
    }
}
