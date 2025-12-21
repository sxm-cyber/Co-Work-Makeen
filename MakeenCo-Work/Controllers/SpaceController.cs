using MakeenCo_Work.Application.Commands;
using MakeenCo_Work.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
    public class SpaceController : BaseApiController
    {
        private readonly ISpaceService _service;

        public SpaceController(ISpaceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpaceCommand command)
        {
            await _service.CreateAsync(command);
            return Ok(new { message = "Space created successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSpaceCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");
            await _service.UpdateAsync(command);
            return Ok(new { message = "Space updated successfully" });
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> SetStatus(Guid id, [FromBody] bool isActive)
        {
            await _service.SetActive(id, isActive);
            return Ok(new { message = "Status updated successfully" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var space = await _service.GetByIdAsync(id);
            if (space == null) return NotFound();
            return Ok(space);
        }
    }
}