using MakeenCo_Work.Domain.Enums;

namespace MakeenCo_Work.Application.DTOs.Message
{
	public class MessageDto
	{
		public Guid Id { get; set; }

		public string Subject { get; set; }

		public string SenderName { get; set; }
		public Guid SenderId { get; set; }

		public Guid RecipientId { get; set; }

		public MessageStatus Status { get; set; }

		public DateTime SentDate { get; set; }

		public bool IsRead { get; set; }

		public MessagePriority Priority { get; set; }

		public MessageType Type { get; set; }
	}
}

