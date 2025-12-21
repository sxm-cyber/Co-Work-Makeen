using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
    public class FaqController : BaseApiController
    {
        private readonly IFaqService _faqService;
        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsyn([FromBody] CreateFaqCommand command)
        {
            await _faqService.CreateFaqAsync(command);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _faqService.GetAllFaqDtoAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _faqService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]UpdateFaqCommand command) 
        {
            await _faqService.UpdateFaqAsync(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _faqService.DeleteFaqAsync(id);
            return Ok();
        }
    }
}
