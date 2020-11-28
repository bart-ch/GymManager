using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymManager.Core.Domain
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        [Required]
        public string Surname { get; set; }

        [StringLength(255)]
        [Required]
        public string JobTitle { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ApplicationUser user &&
                   Email == user.Email &&
                   EmailConfirmed == user.EmailConfirmed &&
                   PasswordHash == user.PasswordHash &&
                   SecurityStamp == user.SecurityStamp &&
                   PhoneNumber == user.PhoneNumber &&
                   PhoneNumberConfirmed == user.PhoneNumberConfirmed &&
                   TwoFactorEnabled == user.TwoFactorEnabled &&
                   LockoutEndDateUtc == user.LockoutEndDateUtc &&
                   LockoutEnabled == user.LockoutEnabled &&
                   AccessFailedCount == user.AccessFailedCount &&
                   EqualityComparer<ICollection<IdentityUserRole>>.Default.Equals(Roles, user.Roles) &&
                   EqualityComparer<ICollection<IdentityUserClaim>>.Default.Equals(Claims, user.Claims) &&
                   EqualityComparer<ICollection<IdentityUserLogin>>.Default.Equals(Logins, user.Logins) &&
                   Id == user.Id &&
                   UserName == user.UserName &&
                   Name == user.Name &&
                   Surname == user.Surname &&
                   JobTitle == user.JobTitle;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public override int GetHashCode()
        {
            int hashCode = 2113194632;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EmailConfirmed.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PasswordHash);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SecurityStamp);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhoneNumber);
            hashCode = hashCode * -1521134295 + PhoneNumberConfirmed.GetHashCode();
            hashCode = hashCode * -1521134295 + TwoFactorEnabled.GetHashCode();
            hashCode = hashCode * -1521134295 + LockoutEndDateUtc.GetHashCode();
            hashCode = hashCode * -1521134295 + LockoutEnabled.GetHashCode();
            hashCode = hashCode * -1521134295 + AccessFailedCount.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<IdentityUserRole>>.Default.GetHashCode(Roles);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<IdentityUserClaim>>.Default.GetHashCode(Claims);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<IdentityUserLogin>>.Default.GetHashCode(Logins);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(JobTitle);
            return hashCode;
        }
    }
}