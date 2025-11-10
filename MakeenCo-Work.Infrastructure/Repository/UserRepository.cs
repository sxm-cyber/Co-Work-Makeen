using Dapper;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;
using MakeenCo_Work.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MakeenCo_Work.Infrastructure.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<User> _userManager;
		private readonly string _connectionString;

		public UserRepository(ApplicationDbContext context , UserManager<User> userManager , IConfiguration configuration)
		{
            _context = context;
            _userManager = userManager;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
		}

   
        public async Task<User?> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM Users WHERE Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }


        public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = " SELECT * FROM Users WHERE PhoneNumber = @PhoneNumber";

            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { PhoneNumber = phoneNumber });
        }



        public async Task<User?> GetByNationalCodeAsync(string nationalCode)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM Users WHERE NationalCode = @NationalCode";

            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { NationalCode = nationalCode });
        }


        public async Task<IEnumerable<User>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            using var connection = new SqlConnection(_connectionString);

            var offset = (pageNumber - 1) * pageSize;

            var sql = @"SELECT * FROM Users 
                       ORDER BY CreatedAt DESC 
                       OFFSET @Offset ROWS 
                       FETCH NEXT @PageSize ROWS ONLY";

            return await connection.QueryAsync<User>(sql, new { Offset = offset, PageSize = pageSize });
        }


        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM Users WHERE IsActive = 1";

            return await connection.QueryAsync<User>(sql);
        }


        public async Task<int> GetTotalCountAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT COUNT(*) FROM Users";

            return await connection.ExecuteScalarAsync<int>(sql);
        }


        public async Task<bool> ExistsAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT COUNT(1) FROM Users WHERE Id = @Id";

            var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });

            return count > 0;
        }


        public async Task<bool> UpdateAsync(User user)
        {
            _context.Users.Update(user);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user is null)
                return false;

            _context.Users.Remove(user);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        
    }
}

