using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Draw.API.Entites
{
    /// <summary>
    /// To keep User entities simple, I prefer to put all user props in one Entity
    /// </summary>
    public class UserEntity : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// In real world scenario, user credentials should be kept seperately
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        /// In real world scenario, user credentials should be kept seperately
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}
