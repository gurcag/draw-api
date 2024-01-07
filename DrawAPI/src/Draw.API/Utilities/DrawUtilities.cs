using Draw.API.Models;

namespace Draw.API.Utilities
{
    public static class DrawUtilities
    {
        public static void PerformDraw(this DrawModel draw, IEnumerable<TeamModel> teams)
        {
            if (draw != null && draw.DrawOptions != null && teams != null && teams.Any())
            {
                teams = teams.OrderBy(x => x.CountryId) // first step is teams are ordered by CountryId
                                .ThenBy(x => new Random().Next()) // then to shuffle teams within per group
                                .ToList();

                int countryCount = teams.DistinctBy(x => x.CountryId).Count();

                int numberOfTeamPerGroup = teams.Count() / draw.DrawOptions.NumberOfGroups;

                var teamsGroupedByCountry = teams.GroupBy(t => t.CountryId).Select(x => x.ToList());

                draw.Groups = new List<DrawGroupModel>();

                for (int i = 0; i < draw.DrawOptions.NumberOfGroups; i++) // looping through groups
                {
                    draw.Groups = draw.Groups.Append(new DrawGroupModel { GroupName = (i + 1).ToString(), Teams = new List<TeamModel>() });

                    for (int j = 0; j < numberOfTeamPerGroup; j++) // looping through within the group
                    {
                        int countryIndex = (j + (numberOfTeamPerGroup * i)) % countryCount;

                        int teamIndex = (j + (numberOfTeamPerGroup * i)) / countryCount;

                        draw.Groups.ElementAt(i).Teams = draw.Groups.ElementAt(i).Teams
                                                        .Append(new TeamModel
                                                        {
                                                            Id = teamsGroupedByCountry.ElementAt(countryIndex).ElementAt(teamIndex).Id
                                                        });
                    }
                }
            }
            else
                throw new ArgumentNullException(nameof(draw));
        }
    }
}
