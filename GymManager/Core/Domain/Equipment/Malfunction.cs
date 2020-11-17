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

        public bool IsRepaired { get; set; }

        public Equipment Equipment { get; set; }
    }
}