using Dapper;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MakeenCo_Work.Infrastructure.Repository
{
	public class DiscountCodeRepository : BaseRepository<DiscountCode> , IDiscountCodeRepository
    {
		private readonly string? _connectionString;

		public DiscountCodeRepository(ApplicationDbContext context , IConfiguration configuration) : base(context)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}


        public async Task<DiscountCode?> GetByCodeAsync(string code)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM DiscountCodes WHERE Code = @Code";

            return await connection.QueryFirstOrDefaultAsync<DiscountCode>(sql, new { Code = code });
        }


        public async Task<IEnumerable<DiscountCode>> GetActiveAsync()
        {
            var now = DateTime.UtcNow;
            
            return await _dbSet
                .Where(dc => dc.IsActive 
                    && dc.ValidFrom <= now 
                    && dc.ValidTo >= now 
                    && (dc.UsageLimit == null || dc.UsedCount < dc.UsageLimit))
                .OrderByDescending(dc => dc.CreatedAt)
                .ToListAsync();
        }


        public async Task<IEnumerable<DiscountCode>> GetExpiredAsync()
        {
            var now = DateTime.UtcNow;

            return await _dbSet
                .Where(dc => !dc.IsActive 
                    || dc.ValidFrom > now 
                    || dc.ValidTo < now 
                    || (dc.UsageLimit != null && dc.UsedCount >= dc.UsageLimit))
                .OrderByDescending(dc => dc.CreatedAt)
                .ToListAsync();
        }


        public async Task<bool> DeactiveAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "UPDATE DiscountCodes SET IsActive = 0, UpdatedAt = GETUTCDATE()";

            var result = await connection.ExecuteAsync(sql);

            return result > 0;
        }
    }
}

