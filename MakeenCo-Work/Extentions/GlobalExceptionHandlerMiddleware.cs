using System.Net;
using System.Text.Json;
using MakeenCo_Work.Application.DTOs;

namespace MakeenCo_Work.Extentions;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger, IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex , "An Unhandled Exception occured. {Message}" , ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        var errorResponse = new ErrorResponseDto
        {
            Path = context.Request.Path,
            TimeStamp = DateTime.UtcNow
        };

        switch (exception)
        {
            case NotFoundException notFoundEx:
                errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Message = notFoundEx.Message;
                errorResponse.Details = _environment.IsDevelopment() ? exception.StackTrace : null;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            
            case BadRequestException badEx:
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = badEx.Message;
                errorResponse.Details = _environment.IsDevelopment() ? exception.StackTrace : null;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            
            case UnauthorizedException unauthorizedEx:
                errorResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.Message = unauthorizedEx.Message;
                errorResponse.Details = _environment.IsDevelopment() ? exception.StackTrace : null;
                response.StatusCode =(int)HttpStatusCode.Unauthorized;
                break;
            
            case ForbiddenException forbiddenEx:
                errorResponse.StatusCode = (int)HttpStatusCode.Forbidden;
                errorResponse.Message = forbiddenEx.Message;
                errorResponse.Details = _environment.IsDevelopment() ? exception.StackTrace : null;
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                break;
            
            case ConflictException conflictEx:
                errorResponse.StatusCode = (int)HttpStatusCode.Conflict;
                errorResponse.Message = conflictEx.Message;
                errorResponse.Details = _environment.IsDevelopment() ? exception.StackTrace : null;
                response.StatusCode = (int)HttpStatusCode.Conflict;
                break;
                
            
            case ArgumentNullException argNullEx:
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = "Invalid Request. Required Parameter is missing.";
                errorResponse.Details = _environment.IsDevelopment() ? argNullEx.Message : null;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            
            case ArgumentException argEx:
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = "Invalid request Parameters.";
                errorResponse.Details = _environment.IsDevelopment() ? argEx.Message : null;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            
            case UnauthorizedAccessException :
                errorResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.Message = "You Are Not Authorized To Perform This Action.";
                errorResponse.Details = _environment.IsDevelopment() ? exception.Message : null;
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            
            case KeyNotFoundException :
                errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Message = "The Requested Resource Not Found.";
                errorResponse.Details = _environment.IsDevelopment() ? exception.Message : null;
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            
            case InvalidOperationException invalidOpEx :
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = "Invalid Operation.";
                errorResponse.Details = _environment.IsDevelopment() ? exception.Message : null;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            
            case Microsoft.EntityFrameworkCore.DbUpdateException dbEx:
                errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = "A DataBase Error Occured While Processing Your Request. ";
                errorResponse.Details = _environment.IsDevelopment() ? dbEx.InnerException?.Message ?? dbEx.Message : null;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                _logger.LogError(dbEx, "DataBase error : {Message}", dbEx.Message);
                break;
            
            case TimeoutException :
                errorResponse.StatusCode = (int)HttpStatusCode.RequestTimeout;
                errorResponse.Message = "The Request Timed Out Please Try Again.";
                errorResponse.Details = _environment.IsDevelopment() ? exception.Message : null;
                response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                break;
            
            default :
                errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "An Error Occured While Proccessing Your Request. ";
                errorResponse.Details = _environment.IsDevelopment() ? exception.ToString() : null;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var jsonResponse = JsonSerializer.Serialize(errorResponse, jsonOptions);
        await response.WriteAsync(jsonResponse);
    }
}