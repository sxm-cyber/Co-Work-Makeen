using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
    public class RegulationsController : BaseApiController
    {
        private readonly IRegulationService _regulationService;

        public RegulationsController(IRegulationService regulationService)
        {
            _regulationService = regulationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRegulationCommand command)
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _regulationService.GetById(id);
            if (result == null)
                return NotFound();

            return Ok(result);
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