using Draw.API.DTOs;

namespace Draw.API.Models
{
    public class DrawModel
    {
        public int Id { get; set; }
        public string DrawerName { get; set; }
        public DateTime DrawDate { get; set; }
        public DrawOptionsModel DrawOptions { get; set; }
        public IEnumerable<DrawGroupModel> Groups { get; set; }
    }
}
