using System;
using MakeenCo_Work.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace MakeenCo_Work.Extentions
{
	public static class DbSeeder
	{
		public static async Task SeedDataAsync(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<User>>();


			//Creating Admin Role
			if(!await roleManager.RoleExistsAsync("Admin"))
			{
				var adminRole = new Role("Admin", "Administrator with full access");
				await roleManager.CreateAsync(adminRole);
			}


			//Creating User Role
			if (!await roleManager.RoleExistsAsync("User"))
			{
				var userRole = new Role("User", "Regular User");
				await roleManager.CreateAsync(userRole);
			}


			//Admin With User Pass
			var adminUsername = "admin";
			var adminPassword = "Admin@123456";
			var adminNationalCode = "0123456789"; 
            var adminPhone = "09000000000";

			var adminUser = await userManager.FindByNameAsync(adminUsername);

			if (adminUser is null)
			{
				adminUser = new User(
					"Admin",
					"User",
					adminNationalCode,
					adminPhone
					);

				adminUser.UserName = adminUsername;
				adminUser.Email = adminNationalCode;

				var result = await userManager.CreateAsync(adminUser, adminPassword);

				if (result.Succeeded)
					await userManager.AddToRoleAsync(adminUser, "Admin");

            }
        }	
	}
}

