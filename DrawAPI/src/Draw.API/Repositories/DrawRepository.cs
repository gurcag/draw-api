using Draw.API.DbContexts;
using Draw.API.Entites;
using Microsoft.EntityFrameworkCore;

namespace Draw.API.Repositories
{
    public class DrawRepository : IDrawRepository
    {
        private readonly DrawDbContext context;

        public DrawRepository(DrawDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> AddDrawAsync(DrawEntity draw)
        {
            await this.context.Draws.AddAsync(draw);

            await this.context.SaveChangesAsync();

            return draw.Id;
        }

        public async Task<DrawEntity?> GetDrawAsync(int Id)
        {
            return await this.context
                                .Draws
                                .Where(x => x.Id == Id)
                                .FirstAsync();
        }

        public async Task<IEnumerable<CountryEntity?>> GetCountriesAsync()
        {
            return await this.context.Countries.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<DrawOptionsEntity?>> GetDrawOptionsAsync()
        {
            return await this.context.DrawOptions.OrderBy(x => x.Id).ToListAsync();

        }

        public async Task<DrawOptionsEntity?> GetDrawOptionAsync(int Id)
        {
            return await this.context
                    .DrawOptions
                    .Where(x => x.Id == Id)
                    .FirstAsync();
        }

        public async Task<IEnumerable<DrawEntity?>> GetDrawsAsync()
        {
            return await this.context.Draws.OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<TeamEntity?>> GetTeamsAsync()
        {
            return await this.context.Teams
                .Include(x => x.Country)
                .OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<TeamEntity?> GetTeamAsync(int Id)
        {
            return await this.context.Teams
                .Include(x => x.Country)
                .Where(x => x.Id == Id)
                .FirstAsync();
        }

        public async Task<UserEntity?> ValidateUserCredentialsAsync(UserEntity user)
        {
            var x = await this.context.Users.FirstOrDefaultAsync(x => string.Equals(x.Username, user.Username) && string.Equals(x.Password, user.Password));
            return x;
        }
    }
}
