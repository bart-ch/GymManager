using System.ComponentModel.DataAnnotations;

namespace GymManager.Core.Domain
{
    public class OrderStatus
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}