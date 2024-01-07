using Draw.API.DTOs;
using Draw.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Draw.API.Controllers
{
    public class TeamsController : BaseController
    {
        public TeamsController(IBusinessService service) : base(service) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var teams = await base.service.GetTeamsAsync();

            if (teams != null)
            {
                return Ok(teams.Select(x => new Team { Name = x.Name }));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
