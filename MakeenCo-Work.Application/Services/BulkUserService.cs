using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using MakeenCo_Work.Application.DTOs;
using MakeenCo_Work.Application.IServices;
using MakeenCo_Work.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace MakeenCo_Work.Application.Services
{
	public class BulkUserService : IBulkUserService
	{
        private readonly UserManager<User> _userManager;
        private readonly ILogger<BulkUserService> _logger;


		public BulkUserService(UserManager<User> userManager , ILogger<BulkUserService> logger)
		{
            _userManager = userManager;
            _logger = logger;
		}


        public async Task<BulkUploadResultDto> ProcessBulkUserUploadAsync(IFormFile file)
        {
            var result = new BulkUploadResultDto();
            var usersToCreate = new List<BulkUserDto>();


            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader , new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null
                }))
                {
                    csv.Context.RegisterClassMap<BulkUserDtoMap>();
                    usersToCreate = csv.GetRecords<BulkUserDto>().ToList();
                }

                result.TotalRecords = usersToCreate.Count;

                foreach(var userDto in usersToCreate)
                {
                    try
                    {
                        //Validate
                        if(string.IsNullOrEmpty(userDto.FirstName) ||
                            string.IsNullOrEmpty(userDto.LastName) ||
                            string.IsNullOrEmpty(userDto.NationalCode) ||
                            string.IsNullOrEmpty(userDto.PhoneNumber))
                        {
                            result.FailureCount++;
                            result.Errors.Add($"Row {usersToCreate.IndexOf(userDto) + 1} : Missing Required Fields");
                            result.FailedRecordes.Add(userDto);
                            continue;
                        }

                        //Check If User Exsist
                        var existingUser = await _userManager.FindByNameAsync(userDto.NationalCode);

                        if(existingUser is not null)
                        {
                            result.FailureCount++;
                            result.Errors.Add($"User With NationalCode{userDto.NationalCode} Already Exists");
                            result.FailedRecordes.Add(userDto);
                            continue;
                        }


                        //Create User
                        var user = new User(
                            userDto.FirstName,
                            userDto.LastName,
                            userDto.NationalCode,
                            userDto.PhoneNumber
                            );

                        user.UserName = userDto.NationalCode;

                        var createResult = await _userManager.CreateAsync(user);

                        if(createResult.Succeeded)
                        {
                            //Assign User Role
                            await _userManager.AddToRoleAsync(user, "User");
                            result.SuccessCount++;

                            _logger.LogInformation($"Successfully Created User : {userDto.NationalCode}");
                        }
                        else
                        {
                            result.FailureCount++;
                            var errors = string.Join(", ", createResult.Errors.Select(x => x.Description));
                            result.Errors.Add($"Failed To Create User {userDto.NationalCode} : {errors}");
                            result.FailedRecordes.Add(userDto);
                        }        
                    }
                    catch(Exception ex)
                    {
                        result.FailureCount++;
                        result.Errors.Add($"Error Prosecceing User {userDto.NationalCode}: {ex.Message}");
                        result.FailedRecordes.Add(userDto);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Processing Bulk User Upload");
                result.Errors.Add($"FileProcessing error : {ex.Message}");
            }

            return result;
        }
    }
}

