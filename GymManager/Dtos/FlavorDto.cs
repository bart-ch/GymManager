using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymManager.Dtos
{
    public class FlavorDto
    {
        public byte Id { get; set; }

        [Required]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is FlavorDto dto &&
                   Id == dto.Id &&
                   Name == dto.Name;
        }

        public override int GetHashCode()
        {
            int hashCode = -1919740922;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}