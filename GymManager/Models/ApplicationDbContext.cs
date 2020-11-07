using GymManager.Core.Domain.Equipment;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GymManager.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Type> Types { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}