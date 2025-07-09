using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Produces("application/json")]
    public class BaseController<TService> : ControllerBase
    {
        protected readonly TService _service;

        public BaseController(TService service)
        {
            _service = service;
        }

        protected long GetUserId()
        {
            long userId = 0;
            try
            {
                var endpoint = HttpContext.GetEndpoint();
                if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() == null)
                {
                    userId = Convert.ToInt64(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
                }
            }
            catch
            {
                throw new AuthorizationException();
            }
            return userId;
        }
    }
}