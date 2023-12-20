using currency_converter.Data.Implementations;
using currency_converter.Data.Models.Dto.SubscriptionDtos;
using currency_converter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace currency_converter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SubscriptionController : BaseController
    {
        private readonly SubscriptionService _service;
        public SubscriptionController(SubscriptionService service) { _service = service; }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll() 
        {
            return Ok(_service.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            SubscriptionDto? subscription = _service.Get(id);
            return subscription is null ? NotFound() : Ok(subscription);
        }
        [HttpPost("change-subscription")]
        public IActionResult ChangeSubscription(int subscriptionId)
        {
            if (!_service.Exists(subscriptionId)) return NotFound();
            _service.UpdateSubscription(UserId(), subscriptionId);
            return NoContent();
        }
        [HttpPost]
        public IActionResult Add(SubscriptionForCreationDto dto)
        {
            if (!Admin()) return Forbid();
            int newSubscriptionId = _service.Add(dto);
            return Created("/subscriptions/", newSubscriptionId);
        }
        [HttpPut]
        public IActionResult Update(SubscriptionForUpdateDto dto)
        {
            if (!Admin()) return Forbid();
            _service.Update(dto);
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete(SubscriptionForDeletionDto dto)
        {
            if (!Admin()) return Forbid();
            _service.Delete(dto);
            return NoContent();
        }
    }
}
