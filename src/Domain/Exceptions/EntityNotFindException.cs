using System.Net;

namespace Domain.Exceptions;

public class EntityNotFindException : BaseException
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;
    public override string Error => "Not Found";

    public EntityNotFindException(string message) : base(message)
    {
    }
}