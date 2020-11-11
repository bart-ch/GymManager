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
        public int InitialAmount { get; set; }

        [Required]
        public int ConsumedAmount { get; set; }

        [Required]
        public int CurrentAmount
        {
            get
            {
                return InitialAmount - CurrentAmount;
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