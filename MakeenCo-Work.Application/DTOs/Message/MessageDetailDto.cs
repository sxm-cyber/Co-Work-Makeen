using MakeenCo_Work.Domain.Enums;

namespace MakeenCo_Work.Application.DTOs
{
	public class MessageDetailDto
	{
		public Guid Id { get; set; }

		public string Subject { get; set; }

		public string Content { get; set; }

		public string SenderName { get; set; }
		public Guid SenderId { get; set; }

		public string? SenderProfiePicture { get; set; }

		public string RecipientName { get; set; }
		public Guid RecipientId { get; set; }

		public DateTime SentDate { get; set; }

		public DateTime? ReadAt { get; set; }

		public bool IsRead { get; set; }

		public MessageStatus Status { get; set; }

		public MessageType Type { get; set; }

		public MessagePriority Priority { get; set; }

		public List<FileUploadDto> AttachedFiles { get; set; } = new List<FileUploadDto>();
	}
}

