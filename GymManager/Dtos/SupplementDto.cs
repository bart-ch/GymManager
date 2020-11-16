using System;
using System.Collections.Generic;
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

        public override bool Equals(object obj)
        {
            return obj is SupplementDto dto &&
                   Id == dto.Id &&
                   Brand == dto.Brand &&
                   InitialAmount == dto.InitialAmount &&
                   ConsumedAmount == dto.ConsumedAmount &&
                   CurrentAmount == dto.CurrentAmount &&
                   DeliveryDate == dto.DeliveryDate &&
                   EqualityComparer<FlavorDto>.Default.Equals(Flavor, dto.Flavor) &&
                   FlavorId == dto.FlavorId &&
                   EqualityComparer<SupplementTypeDto>.Default.Equals(SupplementType, dto.SupplementType) &&
                   SupplementTypeId == dto.SupplementTypeId;
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
            hashCode = hashCode * -1521134295 + EqualityComparer<FlavorDto>.Default.GetHashCode(Flavor);
            hashCode = hashCode * -1521134295 + FlavorId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<SupplementTypeDto>.Default.GetHashCode(SupplementType);
            hashCode = hashCode * -1521134295 + SupplementTypeId.GetHashCode();
            return hashCode;
        }
    }
}