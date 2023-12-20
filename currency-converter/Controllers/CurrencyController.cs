using currency_converter.Data.Implementations;
using currency_converter.Data.Models.Dto.CurrencyDtos;
using currency_converter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace currency_converter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CurrencyController : BaseController
    {
        private readonly CurrencyService _service;
        public CurrencyController(CurrencyService service) { _service = service; }
        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!_service.Exists(id)) return NotFound();
            return Ok(_service.Get(id));
        }
        [HttpGet("get-favorites")]
        public IActionResult GetFavorites()
        {
            return Ok(_service.GetFav(UserId()));
        }
        [HttpPost("add-user-fav")]
        public IActionResult AddFavCurrency([FromBody] int currencyId)
        {
            if (!_service.Exists(currencyId)) return NotFound();
            _service.AddFav(UserId(), currencyId);
            return NoContent();
        }
        [HttpDelete("remove-user-fav")]
        public IActionResult RemoveFavCurrency([FromBody] int currencyId)
        {
            if (!_service.Exists(currencyId)) return NotFound();
            _service.RemoveFav(UserId(), currencyId);
            return NoContent();
        }
        [HttpPost]
        public IActionResult Add([FromBody] CurrencyForCreationDto dto)
        {
            if (!Admin()) return Forbid();
            if (_service.Exists(dto.Name, dto.Symbol)) return Conflict();
            int newCurrencyId = _service.Add(dto);
            return Created("/currencies/", newCurrencyId);
        }
        [HttpPut]
        public IActionResult Update([FromBody] CurrencyForUpdateDto dto)
        {
            if (!Admin()) return Forbid();
            if (!_service.Exists(dto.Id)) return NotFound();
            _service.Update(dto);
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] CurrencyForDeletionDto dto)
        {
            if (!Admin()) return Forbid();
            if (!_service.Exists(dto.Id)) return NotFound();
            _service.Remove(dto);
            return NoContent();
        }
    }
}
