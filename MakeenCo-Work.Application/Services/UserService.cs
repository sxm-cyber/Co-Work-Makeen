using MakeenCo_Work.Application.Command;
using MakeenCo_Work.Application.Command.User;
using MakeenCo_Work.Application.DTOs.User;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Domain.IRepository;
using MakeenCo_Work.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace MakeenCo_Work.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

		public UserService(IUserRepository userRepository , UserManager<User> userManager)
		{
			_userRepository = userRepository;
            _userManager = userManager;
		}

        private UserDto MapToDto(User user)
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
                IsMandatoryCoworking = user.IsMandatoryCoworking,
                CreatedAt = user.CreatedAt,
                LastLoginAt = user.LastLoginAt
            };

        }


        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
                return null;

            return MapToDto(user);
        }



        public async Task<UserDto?> GetByNationalCodeAsync(string nationalCode)
        {
            var user = await _userRepository.GetByNationalCodeAsync(nationalCode);

            if (user is null)
                return null;

            return MapToDto(user);
        }


        public async Task<IEnumerable<UserDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            var users = await _userRepository.GetAllAsync(pageNumber, pageSize);
            return users.Select(MapToDto);
        }


        public async Task<bool> UpdateAsync(Guid id, UpdateUserCommand updateUserCommand)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
                return false;

            user.UpdateProfile(updateUserCommand.FirstName, updateUserCommand.LastName, updateUserCommand.PhoneNumber);
            return await _userRepository.UpdateAsync(user);
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }


        public async Task<bool> ActivateAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
                return false;

            user.SetActive(true);
            return await _userRepository.UpdateAsync(user);
        }


        public async Task<bool> DeactivateAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
                return false;

            user.SetActive(false);
            return await _userRepository.UpdateAsync(user);
        }


        public async Task<bool> SetMandatoryCoworkingAsync(Guid userId , bool isMandatory)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
                return false;

            user.SetMandatoryCoworking(isMandatory);

            return await _userRepository.UpdateAsync(user);
        }


        public async Task<int> GetToTalCountAsync()
        {
            return await _userRepository.GetTotalCountAsync();
        }

        public async Task<UserDto> RegisterByAdminAsync(AdminRegisterUserCommand command)
        {
            //Check User Existing
            var existingUserByNationalCode = await _userRepository.GetByNationalCodeAsync(command.NationalCode);

            if (existingUserByNationalCode is not null)
                throw new Exception("User With This NationalCode Already Exists");

            var existingUserByPhoneNumber = await _userRepository.GetByPhoneNumberAsync(command.PhoneNumber);

            if (existingUserByPhoneNumber is not null)
                throw new Exception("User With This PhoneNumber Already Exists");


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

            return MapToDto(user);
        }
    }
}

