using GymManager.Core.Repositories;
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
        int Complete();
    }
}
