using GymManager.Core.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GymManager.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<Flavor> Flavors { get; set; }
        public DbSet<SupplementType> SupplementTypes { get; set; }
        public DbSet<Malfunction> Malfunctions { get; set; }        
        public DbSet<EquipmentOrder> EquipmentOrders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }


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