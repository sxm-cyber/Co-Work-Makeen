using FluentValidation;
using FluentValidation.AspNetCore;
using MakeenCo_Work.Application.Validators;

namespace MakeenCo_Work.Configurations
{
	public static class FluentValidations
	{
		public static IServiceCollection AddApplicationFluentValidations(this IServiceCollection services)
		{
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();

            return services;
        }
		
	}
}

