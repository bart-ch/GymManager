using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Core.Domain
{
    public class EquipmentOrder
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Brand { get; set; }

        [Required]
        [StringLength(255)]
        public string Model { get; set; }

        [Range(typeof(DateTime), "01/01/2000", "01/01/2100")]
        public DateTime DesiredDeliveryDate { get; set; }

        public Type Type { get; set; }

        [Required]
        public byte TypeId { get; set; }

        public ApplicationUser User { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        [Range(typeof(int), "0", "10000")]
        public int Quantity { get; set; }

        public OrderStatus OrderStatus { get; set; }

        [Required]
        public byte OrderStatusId { get; set; }

    }
}