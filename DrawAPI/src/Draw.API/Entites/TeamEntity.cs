using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Draw.API.Entites
{
    public class TeamEntity : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("CountryId")]
        public CountryEntity Country { get; set; }

        public int CountryId { get; set; }
    }
}
