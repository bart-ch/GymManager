using GymManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GymManager.Core.Repositories
{
    public interface IEquipmentRepository : IRepository<Equipment>
    {
        IEnumerable<Equipment> GetEquipmentWithAreasAndTypes();
        Equipment GetEquipmentWithAreaAndType(Expression<Func<Equipment, bool>> predicate);
    }
}
