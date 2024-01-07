using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Draw.API.Entites
{
    public class DrawEntity : BaseEntity
    {
        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        [ForeignKey("OptionId")]
        public DrawOptionsEntity Options { get; set; }

        public int OptionId { get; set; }

        /// <summary>
        /// Comma and dash seperated drawing result
        /// TeamId,TeamId...-TeamId,TeamId...
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Result { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public UserEntity UserEntity { get; set; }

        public int UserId { get; set; }
    }
}
