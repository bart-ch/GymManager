using System.ComponentModel.DataAnnotations;

namespace GymManager.Core.Domain
{
    public class Flavor
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}