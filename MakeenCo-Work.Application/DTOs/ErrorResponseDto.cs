namespace MakeenCo_Work.Application.DTOs;

public class ErrorResponseDto
{
    public bool Success { get; set; } = false;

    public string Message { get; set; } =  string.Empty;

    public string? Details { get; set; }

    public int StatusCode { get; set; }

    public DateTime TimeStamp { get; set; } =  DateTime.UtcNow;

    public string? Path { get; set; }

    public Dictionary<string , string[]>? Errors { get; set; }
}