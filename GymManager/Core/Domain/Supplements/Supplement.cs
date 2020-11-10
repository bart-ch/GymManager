using System.ComponentModel.DataAnnotations;

namespace GymManager.Core.Domain
{
    public class Supplement
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

        public Flavor Flavor { get; set; }

        [Required]
        public byte FlavorId { get; set; }

        public SupplementType SupplementType { get; set; }

        [Required]
        public byte SupplementTypeId { get; set; }
    }
}