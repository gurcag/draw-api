namespace Draw.API.DTOs
{
    public class DrawGroup
    {
        public string GroupName { get; set; }
        public IEnumerable<Team> Teams { get; set; }
    }
}
