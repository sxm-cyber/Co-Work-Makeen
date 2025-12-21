using MakeenCo_Work.Application.Command.Message;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers
{
	[Authorize]
	public class MessageController : BaseApiController
	{
		private readonly IMessageService _messageService;

		public MessageController(IMessageService messageService)
		{
			_messageService = messageService;
		}
		
		[HttpPost("Send") , Authorize(Roles = "Admin") , Consumes("multipart/form-data")]
		public async Task<IActionResult> SendMessageAsync([FromForm] SendMessageCommand command)
		{
			var senderId = GetCurrentUserId();

			var result = await _messageService.SendMessageAsync(senderId, command);

			if (!result)
			{
				return BadRequest(new
				{
					Message = "Failed To Send Message",
					SenderId = senderId,
					RecipientCount = command.RecipientIds?.Count ?? 0
				});
			}

			return Ok(new
			{
				Message = "Message Sent Successfully",
				SenderId = senderId,
				RecipientCount = command.RecipientIds?.Count ?? 0
			});
		}

		[HttpGet("Inbox")]
		public async Task<IActionResult> GetInboxAsync(
			[FromQuery] MessageStatus? status = null,
			[FromQuery] int pageNumber = 1,
			[FromQuery] int pageSize = 10)
		{
			var userId = GetCurrentUserId();

			var messages = await _messageService.GetInboxAsync(userId, status, pageNumber, pageSize);

			var totalCount = await _messageService.GetInboxCountAsync(userId, status);

			var unreadCount = await _messageService.GetUnreadCountAsync(userId);

			return Ok(new
			{
				Data = messages,
				TotalCount = totalCount,
				UnreadCount = unreadCount,
				PageNumber = pageNumber,
				PageSize = pageSize,
				TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
			});
		}

		[HttpGet("Sent")]
		public async Task<IActionResult> GetSentMessagesAsync([FromQuery] int pageNumber = 1 ,[FromQuery] int pageSize = 10)
		{
			var userId = GetCurrentUserId();

			var messages = await _messageService.GetSentMessagesAsync(userId, pageNumber, pageSize);

			return Ok(new
			{
				Data = messages,
				PageNumber = pageNumber,
				PageSize = pageSize
			});
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var message = await _messageService.GetByIdAsync(id);

			if (message is null)
				return NotFound();

			return Ok(message);
		}

		[HttpPatch("{id}/MarkAsRead")]
		public async Task<IActionResult> MarkAsReadAsync(Guid id)
		{
			var result = await _messageService.MarkAsReadAsync(id);

			if (!result)
				return NotFound();

			return NoContent();
		}

		[HttpPatch("{id}/Approve") , Authorize(Roles = "Admin")]
		public async Task<IActionResult> ApproveMessage(Guid id)
		{
			var result = await _messageService.ApproveMessageAsync(id);

			if (!result)
				return NotFound();

			return Ok(new { Message = "MessageApproved Successfully" });
		}

		[HttpPatch("{id}/Reject") , Authorize(Roles = "Admin")]
		public async Task<IActionResult> RejectMessageAsync(Guid id)
		{
			var result = await _messageService.RejectedMessageAsync(id);

			if (!result)
				return NotFound();

			return Ok(new { Message = "Message Rejected Successfully" });
		}

		[HttpGet("SearchRecipients") , Authorize(Roles = "Admin")]
		public async Task<IActionResult> SearchRecipientsAsync([FromQuery] string search)
		{
			if (string.IsNullOrEmpty(search))
				return BadRequest(new { Message = "Search Term Is Required" });

			var recipients = await _messageService.SearchRecipientsAsync(search);

			return Ok(recipients);
		}

		[HttpDelete("{id}") , Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var result = await _messageService.DeleteAsync(id);

			if (!result)
				return NotFound();

			return NoContent();
		}

	}
}