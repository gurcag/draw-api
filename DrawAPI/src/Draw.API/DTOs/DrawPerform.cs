using System.ComponentModel.DataAnnotations;

namespace Draw.API.DTOs
{
    public class DrawPerform
    {
        [Required]
        public int DrawOptionsId { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password{ get; set; }
    }
}
