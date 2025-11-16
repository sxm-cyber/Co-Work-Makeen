using MakeenCo_Work.Domain.Enums;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.IRepository
{
	public interface IMessageRepository
	{
		Task<Message?> GetByIdAsync(Guid id);

		Task<IEnumerable<Message>> GetInboxAsync(Guid userId, MessageStatus? status = null, int pageNumber = 1, int pageSize = 10);

		Task<IEnumerable<Message>> GetSentMessagesAsync(Guid userId, int pageNumber = 1, int pageSize = 10);

		Task<int> GetInboxCountAsync(Guid userId, MessageStatus? status = null);

		Task<int> GetUnreadCountAsync(Guid userId);

		Task<bool> CreateAsync(Message message);

		Task<bool> CreateBulkAsync(List<Message> messages);

		Task<bool> UpdateAsync(Message message);

		Task<bool> DeleteAsync(Guid id);

		Task<bool> MarkAsReadAsync(Guid id);
	}
}

