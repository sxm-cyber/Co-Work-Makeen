using Dapper;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MakeenCo_Work.Infrastructure.Repository
{
	public class DiscountCodeRepository : IDiscountCodeRepository
    {
		private readonly ApplicationDbContext _context;
		private readonly string? _connectionString;

		public DiscountCodeRepository(ApplicationDbContext context , IConfiguration configuration)
		{
			_context = context;
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}



        public async Task<DiscountCode?> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM DiscountCodes WHERE Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<DiscountCode>(sql, new { Id = id });
        }


        public async Task<DiscountCode?> GetByCodeAsync(string code)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM DiscountCodes WHERE Code = @Code";

            return await connection.QueryFirstOrDefaultAsync<DiscountCode>(sql, new { Code = code });
        }


        public async Task<IEnumerable<DiscountCode>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            using var connection = new SqlConnection(_connectionString);

            var offset = (pageNumber - 1) * pageSize;

            var sql = @"SELECT * FROM DiscountCodes 
                       ORDER BY CreatedAt DESC 
                       OFFSET @Offset ROWS 
                       FETCH NEXT @PageSize ROWS ONLY";

            return await connection.QueryAsync<DiscountCode>(sql, new { Offset = offset, PageSize = pageSize });
        }


        public async Task<IEnumerable<DiscountCode>> GetActiveAsync()
        {
            var now = DateTime.UtcNow;
            
            // Active codes: IsActive = true AND current date is between ValidFrom and ValidTo AND usage limit not reached
            return await _context.DiscountCodes
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
            
            // Expired codes: NOT active OR dates are outside valid range OR usage limit reached
            // A code is expired if it doesn't meet ALL active conditions
            return await _context.DiscountCodes
                .Where(dc => !dc.IsActive 
                    || dc.ValidFrom > now 
                    || dc.ValidTo < now 
                    || (dc.UsageLimit != null && dc.UsedCount >= dc.UsageLimit))
                .OrderByDescending(dc => dc.CreatedAt)
                .ToListAsync();
        }


        public async Task<bool> CreateAsync(DiscountCode discountCode)
        {
            _context.DiscountCodes.Add(discountCode);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }


        public async Task<bool> UpdateAsync(DiscountCode discountCode)
        {
            _context.DiscountCodes.Update(discountCode);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var discountCode = await _context.DiscountCodes.FindAsync(id);

            if (discountCode is null)
                return false;

            _context.DiscountCodes.Remove(discountCode);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }


        public async Task<int> GetTotalCountAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT COUNT(*) FROM DiscountCodes";

            return await connection.ExecuteScalarAsync<int>(sql);
        }


        public async Task<bool> ExistsAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT COUNT(1) FROM DiscountCodes WHERE Id = @Id";

            var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });

            return count > 0;
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

