using Draw.API.DbContexts;
using Draw.API.DTOs;
using Draw.API.Models;
using Draw.API.Repositories;
using Draw.API.Utilities;
using System.Linq;
using System.Text.RegularExpressions;

namespace Draw.API.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IDrawRepository repository;

        public BusinessService(IDrawRepository drawRepository)
        {
            this.repository = drawRepository ?? throw new ArgumentNullException(nameof(drawRepository));
        }
        public async Task<int> PerformDrawAsync(int drawOptionsId, int userId)
        {
            // not the best practise, to make it simplify
            //var userModel = await this.ValidateUserCredentialsAsync(user) ?? throw new ArgumentNullException();

            var drawOptions = await this.GetDrawOptionAsync(drawOptionsId) ?? throw new ArgumentNullException();

            var teams = await this.GetTeamsAsync();

            var drawModel = new DrawModel { DrawOptions = drawOptions };

            drawModel.PerformDraw(teams);

            string drawResultString = "";

            foreach (var group in drawModel.Groups)
            {
                foreach (var team in group.Teams)
                {
                    drawResultString += team.Id + ",";
                }
                drawResultString = drawResultString.Remove(drawResultString.Length - 1) + "-";
            }
            drawResultString = drawResultString.Remove(drawResultString.Length - 1);

            var drawEntity = new Entites.DrawEntity
            {
                DateCreated = DateTime.Now,
                OptionId = drawOptions.Id,
                Result = drawResultString,
                UserId = userId,
            };

            return await this.repository.AddDrawAsync(drawEntity);
        }

        public async Task<IEnumerable<CountryModel?>> GetCountriesAsync()
        {
            var countries = await this.repository.GetCountriesAsync();
            return countries.Select(x => new CountryModel { Id = x.Id, Name = x.Name });
        }

        public async Task<DrawModel?> GetDrawAsync(int Id)
        {
            var drawEntity = await this.repository.GetDrawAsync(Id);

            DrawModel drawModel = new DrawModel();

            if (drawEntity != null)
            {
                var groups = drawEntity.Result.Split('-');

                drawModel.Id = drawEntity.Id;
                drawModel.DrawDate = drawEntity.DateCreated;
                drawModel.DrawerName = drawEntity.UserEntity.Name;
                drawModel.DrawOptions = new DrawOptionsModel { Id = drawEntity.Options.Id, NumberOfGroups = drawEntity.Options.NumberOfGroups };
                drawModel.Groups = new List<DrawGroupModel>();

                for (int i = 0; i < groups.Length; i++)
                {
                    string groupName = (i + 1).ToString();

                    var drawGroupModel = new DrawGroupModel { GroupName = groupName, Teams = new List<TeamModel>() };

                    var teams = groups[i].Split(',');

                    foreach (var teamId in teams)
                    {
                        int teamIdInt = int.Parse(teamId);
                        var teamEntity = this.repository.GetTeamAsync(teamIdInt).Result;
                        drawGroupModel.Teams = drawGroupModel.Teams.Append(new TeamModel { Id = teamIdInt, CountryId = teamEntity.CountryId, Name = teamEntity.Name });
                    }

                    drawModel.Groups = drawModel.Groups.Append(drawGroupModel);

                }
            }

            return drawModel;
        }

        public async Task<IEnumerable<DrawOptionsModel?>> GetDrawOptionsAsync()
        {
            var drawOptions = await this.repository.GetDrawOptionsAsync();
            return drawOptions.Select(x => new DrawOptionsModel { Id = x.Id, NumberOfGroups = x.NumberOfGroups });
        }

        public async Task<DrawOptionsModel?> GetDrawOptionAsync(int Id)
        {
            var drawOptions = await this.repository.GetDrawOptionAsync(Id);
            return new DrawOptionsModel { Id = drawOptions.Id, NumberOfGroups = drawOptions.NumberOfGroups };
        }

        public async Task<IEnumerable<TeamModel?>> GetTeamsAsync()
        {
            var teams = await this.repository.GetTeamsAsync();
            return teams.Select(x => new TeamModel { Id = x.Id, Name = x.Name, CountryId = x.CountryId });
        }

        public async Task<UserModel?> ValidateUserCredentialsAsync(UserModel user)
        {
            var userEntity = await this.repository.ValidateUserCredentialsAsync(
                new Entites.UserEntity { Username = user.Username, Password = user.Password }
                );

            return userEntity != null ? new UserModel { Id = userEntity.Id, Name = userEntity.Name } : null;
        }
    }
}
