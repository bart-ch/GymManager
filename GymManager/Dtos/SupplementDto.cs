using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Dtos
{
    public class SupplementDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Brand { get; set; }

        [Range(typeof(int), "1", "10000")]
        public int InitialAmount { get; set; }

        [Range(typeof(int), "0", "10000")]
        public int? ConsumedAmount { get; set; }

        public int? CurrentAmount
        {
            get
            {
                return InitialAmount - ConsumedAmount;
            }
        }

        [Range(typeof(DateTime), "01/01/2000", "01/01/2100")]
        public DateTime DeliveryDate { get; set; }

        public FlavorDto Flavor { get; set; }

        [Required]
        public byte FlavorId { get; set; }

        public SupplementTypeDto SupplementType { get; set; }

        [Required]
        public byte SupplementTypeId { get; set; }
    }
}