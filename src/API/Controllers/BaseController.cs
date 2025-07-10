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
    }
}