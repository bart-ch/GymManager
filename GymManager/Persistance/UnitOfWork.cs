using GymManager.Core;
using GymManager.Core.Domain;
using GymManager.Core.Repositories;
using GymManager.Persistance.Repositories;

namespace GymManager.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IEquipmentRepository Equipment { get; }
        public IRepository<Area> Areas { get; }
        public IRepository<Core.Domain.Type> Types { get; }
        public ISupplementRepository Supplements { get; }
        public IRepository<Flavor> Flavors { get; }
        public IRepository<SupplementType> SupplementTypes { get; }
        public IMalfunctionRepository Malfunctions { get; }
        public IEquipmentOrderRepository EquipmentOrders { get; }
        public IRepository<OrderStatus> OrderStatuses { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Equipment = new EquipmentRepository(context);
            Areas = new Repository<Area>(context);
            Types = new Repository<Core.Domain.Type>(context);
            Supplements = new SupplementRepository(context);
            Flavors = new Repository<Flavor>(context);
            SupplementTypes = new Repository<SupplementType>(context);
            Malfunctions = new MalfunctionRepository(context);
            EquipmentOrders = new EquipmentOrderRepository(context);
            OrderStatuses = new Repository<OrderStatus>(context);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}