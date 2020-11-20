using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymManager.Dtos
{
    public class MalfunctionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime MalfunctionDate { get; set; }
        public bool IsRepaired { get; set; }
        public EquipmentDto Equipment { get; set; }
        public int EquipmentId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is MalfunctionDto dto &&
                   Id == dto.Id &&
                   Title == dto.Title &&
                   Description == dto.Description &&
                   MalfunctionDate == dto.MalfunctionDate &&
                   IsRepaired == dto.IsRepaired &&
                   EqualityComparer<EquipmentDto>.Default.Equals(Equipment, dto.Equipment) &&
                   EquipmentId == dto.EquipmentId;
        }

        public override int GetHashCode()
        {
            int hashCode = -778066411;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + MalfunctionDate.GetHashCode();
            hashCode = hashCode * -1521134295 + IsRepaired.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<EquipmentDto>.Default.GetHashCode(Equipment);
            hashCode = hashCode * -1521134295 + EquipmentId.GetHashCode();
            return hashCode;
        }
    }
}