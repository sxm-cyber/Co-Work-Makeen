using Dapper;
using MakeenCo_Work.Domain.Enums;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MakeenCo_Work.Infrastructure.Repository
{
    public class MessageRepository : BaseRepository<Message> , IMessageRepository
    {
		private readonly string? _connectionString;


		public MessageRepository(ApplicationDbContext context , IConfiguration configuration) : base(context)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}


        public override async Task<Message?> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM Message WHERE Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<Message>(sql, new { Id = id });
        }


        public async Task<IEnumerable<Message>> GetInboxAsync(Guid userId, MessageStatus? status = null, int pageNumber = 1, int pageSize = 10)
        {
            using var connection = new SqlConnection(_connectionString);

            var offset = (pageNumber - 1) * pageSize;

            var sql = @"SELECT * FROM Message 
                       WHERE RecipientId = @UserId";

            if (status.HasValue)
                sql += " AND Status = @Status";

            sql += @" ORDER BY CreatedAt DESC 
                     OFFSET @Offset ROWS 
                     FETCH NEXT @PageSize ROWS ONLY";

            return await connection.QueryAsync<Message>(sql, new
            {
                UserId = userId,
                Status = status,
                Offset = offset,
                PageSize = pageSize
            });
        }


        public async Task<IEnumerable<Message>> GetSentMessagesAsync(Guid userId, int pageNumber = 1, int pageSize = 10)
        {
            using var connection = new SqlConnection(_connectionString);

            var offset = (pageNumber - 1) * pageSize;

            var sql = @"SELECT * FROM Message 
                       WHERE SenderId = @UserId
                       ORDER BY CreatedAt DESC 
                       OFFSET @Offset ROWS 
                       FETCH NEXT @PageSize ROWS ONLY";

            return await connection.QueryAsync<Message>(sql, new
            {
                UserId = userId,
                Offset = offset,
                PageSize = pageSize
            });
        }


        public async Task<int> GetInboxCountAsync(Guid userId, MessageStatus? status = null)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT COUNT(*) FROM Message WHERE RecipientId = @UserId";

            if (status.HasValue)
                sql += " AND Status = @Status";

            return await connection.ExecuteScalarAsync<int>(sql, new { UserId = userId, Status = status });
        }


        public async Task<int> GetUnreadCountAsync(Guid userId)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT COUNT(*) FROM Message WHERE RecipientId = @UserId AND IsRead = 0";

            return await connection.ExecuteScalarAsync<int>(sql, new { UserId = userId });
        }


        public async Task<bool> CreateBulkAsync(List<Message> messages)
        {
            await _context.Messages.AddRangeAsync(messages);

            return true;
        }


        public async Task<bool> MarkAsReadAsync(Guid id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message is null)
                return false;

            message.MarkAsRead();

            _context.Messages.Update(message);

            return true;
        }
    }
}