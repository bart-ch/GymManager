using GymManager.Core.Domain;
using GymManager.Core.Repositories;
using GymManager.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Core
{
    public interface IUnitOfWork : IDisposable
    {
     
        IEquipmentRepository Equipment { get; }
        IRepository<Area> Areas { get; }
        IRepository<Domain.Type> Types { get; }
        int Complete();
    }
}
