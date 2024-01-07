using Draw.API.DTOs;

namespace Draw.API.Models
{
    public class DrawGroupModel
    {
        public string GroupName { get; set; }
        public IEnumerable<TeamModel> Teams { get; set; }
    }
}
