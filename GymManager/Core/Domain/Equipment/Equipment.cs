using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManager.Core.Domain
{
    [Table("Equipment")]
    public class Equipment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Brand { get; set; }        
        
        [Required] 
        [StringLength(255)]
        public string Model { get; set; }

        [StringLength(255)]
        public string SerialNumber { get; set; }

        [Range(typeof(DateTime), "01/01/2000", "01/01/2100")]
        public DateTime DeliveryDate { get; set; }

        public Area Area { get; set; }
        [Required]
        public byte AreaId { get; set; }

        public Type Type { get; set; }
        [Required]
        public byte TypeId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Equipment equipment &&
                   Id == equipment.Id &&
                   Brand == equipment.Brand &&
                   Model == equipment.Model &&
                   SerialNumber == equipment.SerialNumber &&
                   DeliveryDate == equipment.DeliveryDate &&
                   EqualityComparer<Area>.Default.Equals(Area, equipment.Area) &&
                   AreaId == equipment.AreaId &&
                   EqualityComparer<Type>.Default.Equals(Type, equipment.Type) &&
                   TypeId == equipment.TypeId;
        }

        public override int GetHashCode()
        {
            int hashCode = -1579632172;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Brand);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Model);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SerialNumber);
            hashCode = hashCode * -1521134295 + DeliveryDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Area>.Default.GetHashCode(Area);
            hashCode = hashCode * -1521134295 + AreaId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + TypeId.GetHashCode();
            return hashCode;
        }
    }
}