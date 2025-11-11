using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegulationsController : ControllerBase
    {
        private readonly IRegulationService _regulationService;

        public RegulationsController(IRegulationService regulationService)
        {
            _regulationService = regulationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync( [FromBody] CreateRegulationCommand command)
        {
            await _regulationService.CreateRegulationAsync(command);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() 
        {
            var regul = await _regulationService.GetAllRegulationDtoAsync();
            return Ok(regul);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRegulationAsync([FromBody] UpdateRegulationCommand command)
        {
            await _regulationService.UpdateRegulationAsync(command);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRegulationAsync(Guid Id)
        {
            await _regulationService.DeleteRegulationAsync(Id);
            return Ok();

        }
    }
}
