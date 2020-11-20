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
    }
}