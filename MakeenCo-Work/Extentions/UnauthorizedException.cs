namespace MakeenCo_Work.Extentions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message){}
}