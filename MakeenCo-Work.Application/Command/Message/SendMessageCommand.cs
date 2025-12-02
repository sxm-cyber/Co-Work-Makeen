using MakeenCo_Work.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace MakeenCo_Work.Application.Command.Message
{
	public class SendMessageCommand
	{
		public bool SendToAll { get; set; }

		public List<Guid>? RecipientIds { get; set; }

		public string Subject { get; set; }

		public string Content { get; set; }

		public MessageType Type { get; set; }

		public MessagePriority Priority { get; set; }

		public List<IFormFile>? Files { get; set; }
	}
}

