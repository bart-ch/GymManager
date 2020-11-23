using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Core.Domain
{
    public class Malfunction
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Range(typeof(DateTime), "01/01/2000", "01/01/2100")]
        public DateTime MalfunctionDate { get; set; }

        public bool IsRepaired { get; set; }

        public Equipment Equipment { get; set; }

        [Required]
        public int EquipmentId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Malfunction malfunction &&
                   Id == malfunction.Id &&
                   Title == malfunction.Title &&
                   Description == malfunction.Description &&
                   MalfunctionDate == malfunction.MalfunctionDate &&
                   IsRepaired == malfunction.IsRepaired &&
                   EqualityComparer<Equipment>.Default.Equals(Equipment, malfunction.Equipment) &&
                   EquipmentId == malfunction.EquipmentId;
        }

        public override int GetHashCode()
        {
            int hashCode = -778066411;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + MalfunctionDate.GetHashCode();
            hashCode = hashCode * -1521134295 + IsRepaired.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Equipment>.Default.GetHashCode(Equipment);
            hashCode = hashCode * -1521134295 + EquipmentId.GetHashCode();
            return hashCode;
        }
    }
}