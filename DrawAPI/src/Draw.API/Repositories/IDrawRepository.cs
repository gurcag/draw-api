using Draw.API.Entites;
using Draw.API.Models;

namespace Draw.API.Repositories
{
    public interface IDrawRepository
    {
        Task<IEnumerable<CountryEntity?>> GetCountriesAsync();
        Task<IEnumerable<TeamEntity?>> GetTeamsAsync();
        Task<TeamEntity?> GetTeamAsync(int Id);
        Task<UserEntity?> ValidateUserCredentialsAsync(UserEntity user);
        Task<IEnumerable<DrawOptionsEntity?>> GetDrawOptionsAsync();
        Task<DrawOptionsEntity?> GetDrawOptionAsync(int Id);
        Task<DrawEntity?> GetDrawAsync(int Id);
        Task<IEnumerable<DrawEntity?>> GetDrawsAsync();
        Task<int> AddDrawAsync(DrawEntity draw);
    }
}
