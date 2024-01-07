namespace Draw.API.DTOs
{
    public class Draw
    {
        public int Id { get; set; }
        public string DrawerName { get; set; }
        public DateTime DrawDate { get; set; }
        public DrawOptions DrawOptions { get; set; }
        public IEnumerable<DrawGroup> Groups { get; set; }
    }
}
