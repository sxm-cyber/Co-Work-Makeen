using MakeenCo_Work.Application.Services;
using MakeenCo_Work.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
    public class ReservationController : BaseApiController
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationCommand command)
        {
            var result = await _service.CreateAsync(command);
            return Ok(result);
        }
        
        [HttpPut("{id}/confirm")]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var result = await _service.ConfirmAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id, CancelCommand command)
        {
            var result = await _service.CancelAsync(id, command);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}