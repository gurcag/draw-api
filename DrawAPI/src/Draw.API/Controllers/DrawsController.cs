using Draw.API.DTOs;
using Draw.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Draw.API.Controllers
{
    public class DrawsController : BaseController
    {
        public DrawsController(IBusinessService service) : base(service) { }

        [HttpGet("{drawId}", Name = "GetDrawRoute")]
        public async Task<ActionResult<IEnumerable<Draw.API.DTOs.Draw>>> GetDraw(int drawId)
        {
            var draw = await base.service.GetDrawAsync(drawId);

            if (draw != null)
            {
                return Ok(new Draw.API.DTOs.Draw
                {
                    Id = draw.Id,
                    DrawerName = draw.DrawerName,
                    DrawDate = draw.DrawDate,
                    DrawOptions = new DrawOptions { NumberOfGroups = draw.DrawOptions.NumberOfGroups },
                    Groups = draw.Groups.Select(x => new DrawGroup { GroupName = x.GroupName, Teams = x.Teams.Select(y => new Team { Name = y.Name }) }),
                });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Draw.API.DTOs.Draw>> PerformDraw(DrawPerform drawPerformDto)
        {
            // HTTP 401 Error Response for Unauthorized user
            var user = await this.service.ValidateUserCredentialsAsync(new Models.UserModel { Username = drawPerformDto.Username, Password = drawPerformDto.Password });
            if (user == null) return Unauthorized();

            // HTTP 400 Error Response for invalid DrawOptions
            var drawOption = await this.service.GetDrawOptionAsync(drawPerformDto.DrawOptionsId);
            if (drawOption == null) return BadRequest();

            int drawId = await this.service.PerformDrawAsync(drawPerformDto.DrawOptionsId, user.Id);

            var drawModel = await this.service.GetDrawAsync(drawId);

            var draw = new Draw.API.DTOs.Draw
            {
                Id = drawModel.Id,
                DrawerName = drawModel.DrawerName,
                DrawDate = drawModel.DrawDate,
                DrawOptions = new DrawOptions { NumberOfGroups = drawModel.DrawOptions.NumberOfGroups },
                Groups = drawModel.Groups.Select(x => new DrawGroup { GroupName = x.GroupName, Teams = x.Teams.Select(y => new Team { Name = y.Name }) }),
            };

            // response location will be https://localhost:9999/api/draws/{Id}
            return CreatedAtRoute("GetDrawRoute",
                new
                {
                    drawId = drawId,
                },
                draw);
        }
    }
}
