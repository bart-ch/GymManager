using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Dtos
{
    public class EquipmentDto
    {
        public int Id { get; set; }

        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public AreaDto Area { get; set; }
        public byte AreaId { get; set; }

        public TypeDto Type { get; set; }
        public byte TypeId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EquipmentDto dto &&
                   Id == dto.Id &&
                   Brand == dto.Brand &&
                   Model == dto.Model &&
                   SerialNumber == dto.SerialNumber &&
                   DeliveryDate == dto.DeliveryDate &&
                   EqualityComparer<AreaDto>.Default.Equals(Area, dto.Area) &&
                   AreaId == dto.AreaId &&
                   EqualityComparer<TypeDto>.Default.Equals(Type, dto.Type) &&
                   TypeId == dto.TypeId;
        }

        public override int GetHashCode()
        {
            int hashCode = -1579632172;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Brand);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Model);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SerialNumber);
            hashCode = hashCode * -1521134295 + DeliveryDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<AreaDto>.Default.GetHashCode(Area);
            hashCode = hashCode * -1521134295 + AreaId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<TypeDto>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + TypeId.GetHashCode();
            return hashCode;
        }
    }
}