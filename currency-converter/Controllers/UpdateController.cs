using currency_converter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace currency_converter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UpdateController : Controller
    {
        private readonly UpdateExchangeService _service;
        public UpdateController(UpdateExchangeService service) { _service = service; }
        [HttpGet]
        public IActionResult GetExchangeRates()
        {
            return _service.GetExchangeRates() ? NoContent() : StatusCode(502);
        }
        [HttpPut]
        public IActionResult UpdateExchangeRates()
        {
            return _service.UpdateExchangeRates() ? NoContent() : StatusCode(502);
        }
    }
}
