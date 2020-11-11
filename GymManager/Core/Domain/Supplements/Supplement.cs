using System.ComponentModel.DataAnnotations;

namespace GymManager.Core.Domain
{
    public class Supplement
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

        public Flavor Flavor { get; set; }

        [Required]
        public byte FlavorId { get; set; }

        public SupplementType SupplementType { get; set; }

        [Required]
        public byte SupplementTypeId { get; set; }
    }
}