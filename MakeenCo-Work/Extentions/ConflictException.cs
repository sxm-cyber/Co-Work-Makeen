namespace MakeenCo_Work.Extentions;

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message){}
}