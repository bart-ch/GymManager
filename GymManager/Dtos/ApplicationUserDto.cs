using System.Collections.Generic;

namespace GymManager.Dtos
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ApplicationUserDto dto &&
                   Id == dto.Id &&
                   Name == dto.Name &&
                   Surname == dto.Surname &&
                   JobTitle == dto.JobTitle &&
                   Email == dto.Email;
        }

        public override int GetHashCode()
        {
            int hashCode = 206213773;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(JobTitle);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            return hashCode;
        }
    }
}