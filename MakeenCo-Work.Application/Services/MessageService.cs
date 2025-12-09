using MakeenCo_Work.Application.Command.Message;
using MakeenCo_Work.Application.DTOs;
using MakeenCo_Work.Application.DTOs.Message;
using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Domain.Enums;
using MakeenCo_Work.Domain.Models;

namespace MakeenCo_Work.Application.Services
{
	public class MessageService : IMessageService
	{
        private readonly IUnitOfWork _unitOfWork;
		private readonly IFileStorageService _fileStorageService;

		public MessageService(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
		{
            _unitOfWork = unitOfWork;
			_fileStorageService = fileStorageService;
		}


        public async Task<MessageDetailDto?> GetByIdAsync(Guid id)
        {
            var message = await _unitOfWork.Messages.GetByIdAsync(id);

            if (message is null)
                return null;

            var sender = await _unitOfWork.Users.GetByIdAsync(message.SenderId);

            var recipient = await _unitOfWork.Users.GetByIdAsync(message.RecipientId);

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
            var messages = await _unitOfWork.Messages.GetInboxAsync(userId, status, pageNumber, pageSize);

            var messageDtos = new List<MessageDto>();

            foreach(var message in messages)
            {
                var sender = await _unitOfWork.Users.GetByIdAsync(message.SenderId);
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
            var messages = await _unitOfWork.Messages.GetSentMessagesAsync(userId, pageNumber, pageSize);

            var messageDtos = new List<MessageDto>();

            foreach(var message in messages)
            {
                var recipient = await _unitOfWork.Users.GetByIdAsync(message.RecipientId);
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
            return await _unitOfWork.Messages.GetInboxCountAsync(userId, status);
        }


        public async Task<int> GetUnreadCountAsync(Guid userId)
        {
            return await _unitOfWork.Messages.GetUnreadCountAsync(userId);
        }


        public async Task<List<RecipientSearchDto>> SearchRecipientsAsync(string searchTerm)
        {
            var users = await _unitOfWork.Users.GetAllAsync(1, 50);

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
                var allUsers = await _unitOfWork.Users.GetActiveUsersAsync();

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

            await _unitOfWork.Messages.CreateBulkAsync(messages);

            await _unitOfWork.CompleteAsync();

            return true;
        }


        public async Task<bool> MarkAsReadAsync(Guid messageId)
        {
            await _unitOfWork.Messages.MarkAsReadAsync(messageId);

            await _unitOfWork.CompleteAsync();

            return true;
        }


        public async Task<bool> ApproveMessageAsync(Guid messageId)
        {
            var message = await _unitOfWork.Messages.GetByIdAsync(messageId);

            if (message is null)
                return false;

            message.Approve();

            await _unitOfWork.Messages.UpdateAsync(message);

            await _unitOfWork.CompleteAsync();

            return true;
        }


        public async Task<bool> RejectedMessageAsync(Guid messageId)
        {
            var message = await _unitOfWork.Messages.GetByIdAsync(messageId);

            if (message is null)
                return false;

            message.Reject();

            await _unitOfWork.Messages.UpdateAsync(message);

            await _unitOfWork.CompleteAsync();

            return true;
        }


        public async Task<bool> DeleteAsync(Guid messageId)
        {
            await _unitOfWork.Messages.DeleteAsync(messageId);

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}