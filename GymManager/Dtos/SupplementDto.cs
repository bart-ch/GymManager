using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymManager.Dtos
{
    public class SupplementDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Brand { get; set; }

        [Required]
        public int InitialAmount { get; set; }

        [Required]
        public int ConsumedAmount { get; set; }

        public int CurrentAmount
        {
            get
            {
                return InitialAmount - ConsumedAmount;
            }
        }

        public FlavorDto Flavor { get; set; }

        [Required]
        public byte FlavorId { get; set; }

        public SupplementTypeDto SupplementType { get; set; }

        [Required]
        public byte SupplementTypeId { get; set; }
    }
}