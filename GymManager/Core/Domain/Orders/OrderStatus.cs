using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManager.Core.Domain
{
    [Table("OrderStatuses")]
    public class OrderStatus
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public static readonly byte InProgressId = 1;

    }
}