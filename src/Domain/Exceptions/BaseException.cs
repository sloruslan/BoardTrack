namespace Domain.Exceptions;

public abstract class BaseException : ApplicationException
{
    public abstract int StatusCode { get; }
    public abstract string Error { get; }

    //public virtual List<Error422Detail> Errors { get; }

    protected BaseException(string message) : base(message)
    {

    }
}