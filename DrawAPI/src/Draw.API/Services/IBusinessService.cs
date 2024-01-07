using Draw.API.Entites;
using Draw.API.Models;

namespace Draw.API.Services
{
    public interface IBusinessService
    {
        Task<IEnumerable<CountryModel?>> GetCountriesAsync();
        Task<IEnumerable<TeamModel?>> GetTeamsAsync();
        Task<IEnumerable<DrawOptionsModel?>> GetDrawOptionsAsync();
        Task<DrawOptionsModel?> GetDrawOptionAsync(int Id);
        Task<DrawModel?> GetDrawAsync(int Id);
        Task<int> PerformDrawAsync(int drawOptionsId, int userId);
        Task<UserModel?> ValidateUserCredentialsAsync(UserModel user);
    }
}
