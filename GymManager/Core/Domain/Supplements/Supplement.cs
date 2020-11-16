using GymManager.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Core.Domain
{
    public class Supplement
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Brand { get; set; }

        [Range(typeof(int), "1", "10000")]
        public int InitialAmount { get; set; }

        [ConsumedAmountLessOrEqualToInitial]
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

        public Flavor Flavor { get; set; }

        [Required]
        public byte FlavorId { get; set; }

        public SupplementType SupplementType { get; set; }

        [Required]
        public byte SupplementTypeId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Supplement supplement &&
                   Id == supplement.Id &&
                   Brand == supplement.Brand &&
                   InitialAmount == supplement.InitialAmount &&
                   ConsumedAmount == supplement.ConsumedAmount &&
                   CurrentAmount == supplement.CurrentAmount &&
                   DeliveryDate == supplement.DeliveryDate &&
                   EqualityComparer<Flavor>.Default.Equals(Flavor, supplement.Flavor) &&
                   FlavorId == supplement.FlavorId &&
                   EqualityComparer<SupplementType>.Default.Equals(SupplementType, supplement.SupplementType) &&
                   SupplementTypeId == supplement.SupplementTypeId;
        }

        public override int GetHashCode()
        {
            int hashCode = 1016347869;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Brand);
            hashCode = hashCode * -1521134295 + InitialAmount.GetHashCode();
            hashCode = hashCode * -1521134295 + ConsumedAmount.GetHashCode();
            hashCode = hashCode * -1521134295 + CurrentAmount.GetHashCode();
            hashCode = hashCode * -1521134295 + DeliveryDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Flavor>.Default.GetHashCode(Flavor);
            hashCode = hashCode * -1521134295 + FlavorId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<SupplementType>.Default.GetHashCode(SupplementType);
            hashCode = hashCode * -1521134295 + SupplementTypeId.GetHashCode();
            return hashCode;
        }
    }
}