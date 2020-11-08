using GymManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.Core.Repositories
{
    public interface IEquipmentRepository : IRepository<Equipment>
    {
        IEnumerable<Equipment> GetEquipmentWithAreasAndTypes();
        Equipment GetSingleOrDefaultEquipmentWithAreaAndType(Expression<Func<Equipment, bool>> predicate);
    }
}
