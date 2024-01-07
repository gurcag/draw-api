using Draw.API.DTOs;
using Draw.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Draw.API.Controllers
{
    public class CountriesController : BaseController
    {
        public CountriesController(IBusinessService service) : base(service) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            var countries = await base.service.GetCountriesAsync();

            if (countries != null)
            {
                return Ok(countries.Select(x => new Country { Name = x.Name }));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
