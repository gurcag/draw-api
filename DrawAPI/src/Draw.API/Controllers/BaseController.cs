using Draw.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Draw.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IBusinessService service;

        public BaseController(IBusinessService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }
    }
}
