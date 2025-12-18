using Microsoft.AspNetCore.Builder;

namespace MakeenCo_Work.Extentions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApplicationMiddleWare(this WebApplication app,
        IWebHostEnvironment environment)
    {
        //Global Exception Musr Be The First MiddleWare
        app.UseGlobalExceptionHandler();

        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}