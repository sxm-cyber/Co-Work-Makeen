using MakeenCo_Work.Application.Command.Message;
using MakeenCo_Work.Application.DTOs;
using MakeenCo_Work.Application.DTOs.Message;
using MakeenCo_Work.Domain.Enums;

namespace MakeenCo_Work.Application.IServices
{
	public interface IMessageService
	{
		Task<MessageDetailDto?> GetByIdAsync(Guid id);

		Task<IEnumerable<MessageDto>> GetInboxAsync(Guid userId, MessageStatus? status = null, int pageNumber = 1, int pageSize = 10);

		Task<IEnumerable<MessageDto>> GetSentMessagesAsync(Guid userId, int pageNumber = 1, int pageSize = 10);

		Task<int> GetInboxCountAsync(Guid userId, MessageStatus? status = null);

		Task<int> GetUnreadCountAsync(Guid userId);

		Task<List<RecipientSearchDto>> SearchRecipientsAsync(string searchTerm);

		Task<bool> SendMessageAsync(Guid senderId, SendMessageCommand command);

		Task<bool> MarkAsReadAsync(Guid messageId);

		Task<bool> ApproveMessageAsync(Guid messageId);

		Task<bool> RejectedMessageAsync(Guid messageId);

		Task<bool> DeleteAsync(Guid messageId);
	}
}

