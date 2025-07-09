using System.Net;

namespace Domain.Exceptions;

public class AuthorizationException : BaseException
{
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;
    public override string Error => "Ошибка доступа";

    public AuthorizationException(string? errorDetail = "Истек срок действия токена или токен отсутствует") : base(errorDetail!)
    {

    }

}