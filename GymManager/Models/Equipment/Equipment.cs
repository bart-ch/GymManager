using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GymManager.Models
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

        public DateTime PurchaseDate { get; set; }

        public Area Area { get; set; }
        [Required]
        public byte AreaId { get; set; }

        public Type Type { get; set; }
        [Required]
        public byte TypeId { get; set; }
    }
}