using MakeenCo_Work.Application.Command.DiscountCode;
using MakeenCo_Work.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize(Roles = "Admin")]
    public class TieredDiscountController : ControllerBase
	{
		private readonly ITieredDiscountService _tieredDiscountService;

		public TieredDiscountController(ITieredDiscountService tieredDiscountService)
		{
			_tieredDiscountService = tieredDiscountService;
		}


		[HttpGet]
		public async Task<IActionResult> GetActiveAsync()
		{
			var tieredDiscount = await _tieredDiscountService.GetActiveAsync();

			if (tieredDiscount is null)
				return NotFound();

			return Ok(tieredDiscount);
		}


		[HttpPost]
		public async Task<IActionResult> CreateOrUpdateAsync([FromBody] UpdateTieredDiscountCommand command)
		{
			var tieredDiscount = await _tieredDiscountService.CreateOrUpdateAsync(command);

			return Ok(tieredDiscount);
		}


		[HttpPatch("{id}/toggle")]
		public async Task<IActionResult> SetActivateAsync(Guid id , [FromBody] bool isActive)
		{
			var result = await _tieredDiscountService.SetActiveAsync(id, isActive);

			if (!result)
				return NotFound();

			return NoContent();
		}


        [HttpPost("calculate")]
		public async Task<IActionResult> CalculatePaidDaysAsync([FromBody] CalculatePaidDaysCommand command)
		{
			var paidDays = await _tieredDiscountService.CalculatePaidDaysAsync(command.ReservedDays);

			return Ok(new { ReserveDays = command.ReservedDays, PaidDays = paidDays });
		}
    }
}

