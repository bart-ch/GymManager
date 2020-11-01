using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymManager.Dtos
{
    public class EquipmentDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Brand { get; set; }

        [Required]
        [StringLength(255)]
        public string Model { get; set; }

        public DateTime PurchaseDate { get; set; }

        [Required]
        public AreaDto Area { get; set; }

        [Required]
        public TypeDto Type { get; set; }
    }
}