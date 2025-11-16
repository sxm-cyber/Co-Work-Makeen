using MakeenCo_Work.Application.Command.Message;
using MakeenCo_Work.Application.DTOs;
using MakeenCo_Work.Application.DTOs.Message;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Domain.Enums;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Application.Services
{
	public class MessageService : IMessageService
	{
		private readonly IMessageRepository _messageRepository;
		private readonly IUserRepository _userRepository;
		private readonly IFileStorageService _fileStorageService;

		public MessageService(IMessageRepository messageRepository , IUserRepository userRepository , IFileStorageService fileStorageService)
		{
			_messageRepository = messageRepository;
			_userRepository = userRepository;
			_fileStorageService = fileStorageService;
		}


        public async Task<MessageDetailDto?> GetByIdAsync(Guid id)
        {
            var message = await _messageRepository.GetByIdAsync(id);

            if (message is null)
                return null;

            var sender = await _userRepository.GetByIdAsync(message.SenderId);

            var recipient = await _userRepository.GetByIdAsync(message.RecipientId);

            return new MessageDetailDto
            {
                Id = message.Id,
                Subject = message.Subject,
                Content = message.Content,
                SenderName = sender?.FullName ?? "Unknown",
                SenderId = message.SenderId,
                SenderProfiePicture = sender?.ProfilePictureUrl,
                RecipientName = recipient?.FullName ?? "Unknown",
                RecipientId = message.RecipientId,
                SentDate = message.CreatedAt,
                ReadAt = message.ReadAt,
                IsRead = message.IsRead,
                Status = message.Status,
                Type = message.Type,
                Priority = message.Priority,
                AttachedFiles = message.GetAttachedFiles().Select(path => new FileUploadDto
                {
                    FilePath = path,
                    FileName = Path.GetFileName(path)
                }).ToList()
            };
        }


        public async Task<IEnumerable<MessageDto>> GetInboxAsync(Guid userId, MessageStatus? status = null, int pageNumber = 1, int pageSize = 10)
        {
            var messages = await _messageRepository.GetInboxAsync(userId, status, pageNumber, pageSize);

            var messageDtos = new List<MessageDto>();

            foreach(var message in messages)
            {
                var sender = await _userRepository.GetByIdAsync(message.SenderId);
                messageDtos.Add(new MessageDto
                {
                    Id = message.Id,
                    Subject = message.Subject,
                    SenderName = sender?.FullName ?? "Unknown",
                    SenderId = message.SenderId,
                    RecipientId = message.RecipientId,
                    Status = message.Status,
                    SentDate = message.CreatedAt,
                    IsRead = message.IsRead,
                    Type = message.Type,
                    Priority = message.Priority
                });
            }

            return messageDtos;
        }


        public async Task<IEnumerable<MessageDto>> GetSentMessagesAsync(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            var messages = await _messageRepository.GetSentMessagesAsync(userId, pageNumber, pageSize);

            var messageDtos = new List<MessageDto>();

            foreach(var message in messages)
            {
                var recipient = await _userRepository.GetByIdAsync(message.RecipientId);
                messageDtos.Add(new MessageDto
                {
                    Id = message.Id,
                    Subject = message.Subject,
                    SenderName = "You",
                    SenderId = message.SenderId,
                    RecipientId = message.RecipientId,
                    Status = message.Status,
                    SentDate = message.CreatedAt,
                    IsRead = message.IsRead,
                    Type = message.Type,
                    Priority = message.Priority
                });
            }

            return messageDtos;
        }


        public async Task<int> GetInboxCountAsync(Guid userId, MessageStatus? status = null)
        {
            return await _messageRepository.GetInboxCountAsync(userId, status);
        }


        public async Task<int> GetUnreadCountAsync(Guid userId)
        {
            return await _messageRepository.GetUnreadCountAsync(userId);
        }


        public async Task<List<RecipientSearchDto>> SearchRecipientsAsync(string searchTerm)
        {
            var users = await _userRepository.GetAllAsync(1, 50);

            var filtered = users.Where(u =>
            u.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            u.NationalCode.Contains(searchTerm) ||
            u.PhoneNumber.Contains(searchTerm)

            ).ToList();


            return filtered.Select(u => new RecipientSearchDto
            {
                Id = u.Id,
                FullName = u.FullName,
                NationalCode = u.NationalCode,
                PhoneNumber = u.PhoneNumber

            }).ToList();
        }


        public async Task<bool> SendMessageAsync(Guid senderId, SendMessageCommand command)
        {
            List<Guid> recipientIds;

            if(command.SendToAll)
            {
                var allUsers = await _userRepository.GetActiveUsersAsync();

                recipientIds = allUsers.Select(u => u.Id).ToList();
            }
            else
            {
                if (command.RecipientIds is null || !command.RecipientIds.Any())
                    throw new Exception("No Recipients Specified");

                recipientIds = command.RecipientIds;
            }

            List<string> uploadedFilePaths = new List<string>();

            if(command.Files is not null && command.Files.Count > 0)
            {
                var uploadedFiles = await _fileStorageService.UploadUserFilesAsync(senderId, command.Files);

                uploadedFilePaths = uploadedFiles.Select(f => f.FilePath).ToList();
            }

            var messages = new List<Message>();

            foreach(var recipientId in recipientIds)
            {
                var message = new Message(
                    command.Subject,
                    command.Content,
                    senderId,
                    recipientId,
                    command.Type,
                    command.Priority
                    );

                if (uploadedFilePaths.Any())
                    message.AttachFiles(uploadedFilePaths);

                messages.Add(message);
            }

            return await _messageRepository.CreateBulkAsync(messages);
        }


        public async Task<bool> MarkAsReadAsync(Guid messageId)
        {
            return await _messageRepository.MarkAsReadAsync(messageId);
        }


        public async Task<bool> ApproveMessageAsync(Guid messageId)
        {
            var message = await _messageRepository.GetByIdAsync(messageId);

            if (message is null)
                return false;

            message.Approve();

            return await _messageRepository.UpdateAsync(message);
        }


        public async Task<bool> RejectedMessageAsync(Guid messageId)
        {
            var message = await _messageRepository.GetByIdAsync(messageId);

            if (message is null)
                return false;

            message.Reject();

            return await _messageRepository.UpdateAsync(message);
        }


        public async Task<bool> DeleteAsync(Guid messageId)
        {
            return await _messageRepository.DeleteAsync(messageId);
        }
    }
}

