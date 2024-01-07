using Draw.API.DTOs;
using Draw.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Draw.API.Controllers
{
    public class DrawOptionsController : BaseController
    {
        public DrawOptionsController(IBusinessService service) : base(service) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetDrawOptions()
        {
            var drawOptions = await base.service.GetDrawOptionsAsync();

            return drawOptions != null ? Ok(drawOptions) : NotFound();
        }
    }
}
