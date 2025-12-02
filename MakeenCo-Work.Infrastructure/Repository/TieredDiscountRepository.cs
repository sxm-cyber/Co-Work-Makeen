using Dapper;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MakeenCo_Work.Infrastructure.Repository
{
	public class TieredDiscountRepository : ITieredDiscountRepository
    {
		private readonly ApplicationDbContext _context;
		private readonly string? _connectionString;

		public TieredDiscountRepository(ApplicationDbContext context , IConfiguration configuration)
		{
			_context = context;
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}


        public async Task<TieredDiscount?> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM TieredDiscounts WHERE Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<TieredDiscount>(sql, new { Id = id });
        }


        public async Task<TieredDiscount?> GetActiveAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT TOP 1 * FROM TieredDiscounts WHERE IsActive = 1 ORDER BY CreatedAt DESC";

            return await connection.QueryFirstOrDefaultAsync<TieredDiscount>(sql);
        }


        public async Task<bool> CreateAsync(TieredDiscount tieredDiscount)
        {
            _context.TieredDiscounts.Add(tieredDiscount);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }


        public async Task<bool> UpdateAsync(TieredDiscount tieredDiscount)
        {
            _context.TieredDiscounts.Update(tieredDiscount);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }


        public async Task<bool> ExistsAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT COUNT(1) FROM TieredDiscounts WHERE Id = @Id";

            var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });

            return count > 0;
        }
    }
}

