using MakeenCo_Work.Application.Command.DiscountCode;
using MakeenCo_Work.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
	[ApiController, Route("api/[controller]"), Authorize(Roles = "Admin")]
	public class DiscountCodeController : ControllerBase
	{
		private readonly IDiscountCodeService _discountCodeService;

		public DiscountCodeController(IDiscountCodeService discountCodeService)
		{
			_discountCodeService = discountCodeService;
		}


		[HttpGet]
		public async Task<IActionResult> GetAllAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
		{
			var discountCode = await _discountCodeService.GetAllAsync(pageNumber, pageSize);

			var totalCount = await _discountCodeService.GetTotalCountAsync();

			var result = new
			{
				Data = discountCode,
				TotalCount = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize,
				TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
			};

			return Ok(result);
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var discountCode = await _discountCodeService.GetByIdAsync(id);

			if (discountCode is null)
				return NotFound();

			return Ok(discountCode);
		}


		[HttpGet("active")]
		public async Task<IActionResult> GetActiveAsync()
		{
			var discountCode = await _discountCodeService.GetActiveAsync();

			return Ok(discountCode);
		}


		[HttpGet("expired")]
		public async Task<IActionResult> GetExpiredAsync()
		{
			var discountCode = await _discountCodeService.GetExpiredAsync();

			return Ok(discountCode);
		}


		[HttpPost]
		public async Task<IActionResult> CreateAsync([FromBody] CreateDiscountCodeCommand command)
		{
			var discountCode = await _discountCodeService.CreateAsync(command);

			return Created($"/api/DiscountCode/{discountCode.Id}", discountCode);
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(Guid id ,[FromBody] UpdateDiscountCodeCommand command)
		{
			var result = await _discountCodeService.UpdateAsync(id, command);

			if (!result)
				return NotFound();

			return NoContent();
		}


        [HttpPatch("{id}/toggle")]
		public async Task<IActionResult> ToggleActiveAsync(Guid id )
		{
			var result = await _discountCodeService.ToggleActiveAsync(id);

			if (!result)
				return NotFound();

			return NoContent();
		}


        [HttpPost("deactivate-all")]
		public async Task<IActionResult> DeactivateAllAsync()
		{
			var result = await _discountCodeService.DeactiveAllAsync();

			return Ok(new { Message = "All Discount Codes Deactived", Success = result });
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var result = await _discountCodeService.DeleteAsync(id);

			if (!result)
				return NotFound();

			return NoContent();
		}
    }
}

