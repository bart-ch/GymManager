using System;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Dtos
{
    public class SupplementDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public int InitialAmount { get; set; }
        public int? ConsumedAmount { get; set; }
        public int? CurrentAmount
        {
            get
            {
                return InitialAmount - ConsumedAmount;
            }
        }
        public DateTime DeliveryDate { get; set; }
        public FlavorDto Flavor { get; set; }
        public byte FlavorId { get; set; }
        public SupplementTypeDto SupplementType { get; set; }
        public byte SupplementTypeId { get; set; }
    }
}