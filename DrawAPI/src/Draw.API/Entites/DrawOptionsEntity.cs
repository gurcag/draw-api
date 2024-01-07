using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Draw.API.Entites
{
    /// <summary>
    /// Can be extended with DrawingStage(Group, Final, Playoff), DrawingStageOptions, DrawingRules and etc.
    /// </summary>
    public class DrawOptionsEntity : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public byte NumberOfGroups { get; set; }
    }
}
