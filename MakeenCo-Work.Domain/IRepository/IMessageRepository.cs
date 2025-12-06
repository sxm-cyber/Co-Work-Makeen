using MakeenCo_Work.Domain.Enums;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Domain.IRepository
{
	public interface IMessageRepository : IBaseRepository<Message>
	{

		Task<IEnumerable<Message>> GetInboxAsync(Guid userId, MessageStatus? status = null, int pageNumber = 1, int pageSize = 10);

		Task<IEnumerable<Message>> GetSentMessagesAsync(Guid userId, int pageNumber = 1, int pageSize = 10);

		Task<int> GetInboxCountAsync(Guid userId, MessageStatus? status = null);

		Task<int> GetUnreadCountAsync(Guid userId);

		Task<bool> CreateBulkAsync(List<Message> messages);

		Task<bool> MarkAsReadAsync(Guid id);
	}
}