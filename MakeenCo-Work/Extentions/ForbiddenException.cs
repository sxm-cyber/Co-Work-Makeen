namespace MakeenCo_Work.Extentions;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base (message){}
}