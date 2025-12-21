using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
    public class WaysOfCommunicationController : BaseApiController
    {
        private readonly IWaysOfCommunicationService _waysOfCommunicationService;
        public WaysOfCommunicationController(IWaysOfCommunicationService waysOfCommunicationService)
        {
            _waysOfCommunicationService = waysOfCommunicationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateWaysOfCommunicationCommand command)
        {
            await _waysOfCommunicationService.CreateWaysOfCommunicationAsync(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var resalt = await _waysOfCommunicationService.GetAllWaysOfCommunicationAsync();
            return Ok(resalt);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _waysOfCommunicationService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateWaysOfCommunicationCommand command)
        {
            await _waysOfCommunicationService.UpdateWaysOfCommunicationAsync(command);
            return Ok();
        }
    }
}