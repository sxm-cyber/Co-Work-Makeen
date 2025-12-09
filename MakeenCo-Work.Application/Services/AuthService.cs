using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.DTOs.Auth;
using MakeenCo_Work.Application.DTOs.User;
using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MakeenCo_Work.Application.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<User> _userManager;
		private readonly IConfiguration _configuration;
        private readonly IOtpService _otpService;


		public AuthService(IUnitOfWork unitOfWork, UserManager<User> userManager , IConfiguration configuration , IOtpService otpService)
		{
            _unitOfWork = unitOfWork;
			_userManager = userManager;
			_configuration = configuration;
            _otpService = otpService;
		}


        private UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                NationalCode = user.NationalCode,
                PhoneNumber = user.PhoneNumber,
                ProfilePictureUrl = user.ProfilePictureUrl,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                LastLoginAt = user.LastLoginAt
            };
        }


        public async Task<LoginResponseDto?> AdminLoginAsync(AdminLoginCommand command)
        {
            var user = await _userManager.FindByNameAsync(command.Username);

            if (user is null)
                return null;

            var result = await _userManager.CheckPasswordAsync(user, command.Password);

            if (!result)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("Admin"))
                return null;

            user.UpdateLastLogin();

            await _unitOfWork.Users.UpdateAsync(user);

            await _unitOfWork.CompleteAsync();

            var userDto = MapToUserDto(user);

            var token = GenerateJwtToken(userDto, roles);

            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(24),
                User = userDto
            };
        }


        public async Task<OtpResponseDto> RequestOtpAsync(RequestOtpCommand command)
        {
            var otp = await _otpService.GenerateOtpAsync(command.PhoneNumber);

            await _otpService.SendSmsAsync(command.PhoneNumber, otp);

            var expirySeconds = _configuration.GetValue<int>("Otp:ExpiryInSeconds", 120);

            return new OtpResponseDto
            {
                Success = true,
                Message = $"Verification Code For Number : {command.PhoneNumber} Have Been Sent",
                ExpiresInSeconds = expirySeconds
            };
        }


        public async Task<VerifyOtpResponseDto> VerifyOtpAsync(VerifyOtpCommand command)
        {
            //Check Otp Correction
            var isOtpValid = await _otpService.ValidateOtpAsync(command.PhoneNumber, command.Otp);

            if (!isOtpValid)
                return new VerifyOtpResponseDto
                {
                    IsValid = false,
                    UserExists = false,
                    Message = "The Input Code Is Not Correct"
                };

            //Check User Existing
            var user = await _unitOfWork.Users.GetByPhoneNumberAsync(command.PhoneNumber);

            if (user is not null)
            {
                var role = await _userManager.GetRolesAsync(user);

                var userDto = MapToUserDto(user);

                var token = GenerateJwtToken(userDto, role);

                user.UpdateLastLogin();

                await _unitOfWork.Users.UpdateAsync(user);

                await _unitOfWork.CompleteAsync();

                return new VerifyOtpResponseDto
                {
                    IsValid = true,
                    UserExists = true,
                    Token = token,
                    User = userDto,
                    Message = "Login Successful"
                };
            }
            else
            {
                await _otpService.MarkPhoneAsVerifiedAsync(command.PhoneNumber);

                return new VerifyOtpResponseDto
                {
                    IsValid = true,
                    UserExists = false,
                    Message = "Please Complete Your Information"
                };
            }
        }


        public async Task<LoginResponseDto?> CompleteRegistrationAsync(CompleteRegistrationCommand command)
        {
            //Verify Phone
            var isPhoneVerified = await _otpService.IsPhoneVerifiedAsync(command.PhoneNumber);

            if (!isPhoneVerified)
                throw new Exception("PhoneNumber Not Verified. Please Complete OTP Verification First.");

            //check User Existing
            var existingUser = await _unitOfWork.Users.GetByPhoneNumberAsync(command.PhoneNumber);

            if (existingUser is not null)
                throw new Exception("User Alreday Exists With This PhoneNumber");

            //Create User
            var user = new User(
                command.FirstName,
                command.LastName,
                command.NationalCode,
                command.PhoneNumber
                );

            user.UserName = command.NationalCode;

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            //Assign User Role
            await _userManager.AddToRoleAsync(user, "User");

            await _otpService.ClearPhoneVerificationAsync(command.PhoneNumber);

            //Generate Token
            var roles = await _userManager.GetRolesAsync(user);
            var userDto = MapToUserDto(user);
            var token = GenerateJwtToken(userDto, roles);

            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(24),
                User = userDto
            };
        }


        public async Task<bool> LogoutAsync(Guid userId)
        {
            return await Task.FromResult(true);
        }


        public string GenerateJwtToken(UserDto user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Name , user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
            };

            //Adding role to claim
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Generate Token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24), // expire 24 hour later it should update to 3 day
                signingCredentials : creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}