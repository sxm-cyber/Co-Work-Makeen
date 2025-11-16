using System.ComponentModel.DataAnnotations;
using MakeenCo_Work.Domain.Enums;

namespace MakeenCo_Work.Domain.Models
{
	public class Message
	{
		public Guid Id { get; private set; }


		[Required , MaxLength(500)]
		public string Subject { get; private set; }

		[Required]
		public string Content { get; private set; }

		public MessageType Type { get; private set; }

		public MessagePriority Priority { get; private set; }

		public bool IsRead { get; private set; }

		public DateTime? ReadAt { get; private set; }

		public DateTime CreatedAt { get; private set; }

		public DateTime? UpdateAt { get; private set; }

		public MessageStatus Status { get; private set; }

		public string? AttachedFilesJson { get; private set; }


		public Guid SenderId { get; private set; }
		public User Sender { get; private set; } = null!;


		public Guid RecipientId { get; private set; }
		public User Recipient { get; private set; } = null!;


		private Message() { }


		public Message(string subject , string content , Guid senderId , Guid recipientId ,
			MessageType type = MessageType.General , MessagePriority priority = MessagePriority.Normal)
		{
			Id = Guid.NewGuid();
			Subject = subject;
			Content = content;
			SenderId = senderId;
			RecipientId = recipientId;
			Type = type;
			Priority = priority;
			IsRead = false;
			Status = MessageStatus.Pending;
			CreatedAt = DateTime.UtcNow;
		}

		public void MarkAsRead()
		{
			IsRead = true;
			ReadAt = DateTime.UtcNow;
		}

		public void Update(string subject , string content)
		{
			Subject = subject;
			Content = content;
			UpdateAt = DateTime.UtcNow;
		}

		public void SetStatus(MessageStatus status)
		{
			Status = status;
			UpdateAt = DateTime.UtcNow;
		}

		public void Approve()
		{
			Status = MessageStatus.Approved;
			UpdateAt = DateTime.UtcNow;
		}

		public void Reject()
		{
			Status = MessageStatus.Rejected;
			UpdateAt = DateTime.UtcNow;
		}

		public void AttachFiles(List<string> filePaths)
		{
			AttachedFilesJson = System.Text.Json.JsonSerializer.Serialize(filePaths);
			UpdateAt = DateTime.UtcNow;
		}

		public List<string> GetAttachedFiles()
		{
			if (string.IsNullOrEmpty(AttachedFilesJson))
				return new List<string>();

			return System.Text.Json.JsonSerializer.Deserialize<List<string>>(AttachedFilesJson) ?? new List<string>();
		}
	}
}

