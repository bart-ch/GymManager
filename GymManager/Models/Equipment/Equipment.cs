using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymManager.Models
{
    public class Equipment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string TypeOfEquipment { get; set; }

        [Required]
        [StringLength(255)]
        public string Brand { get; set; }        
        
        [Required]
        [StringLength(255)]
        public string Model { get; set; }

        public DateTime PurchaseDate { get; set; }

        public Area Area { get; set; }
    }
}