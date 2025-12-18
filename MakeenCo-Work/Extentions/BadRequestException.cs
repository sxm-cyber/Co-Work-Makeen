namespace MakeenCo_Work.Extentions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message){}

}