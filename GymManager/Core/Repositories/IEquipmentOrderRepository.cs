using GymManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GymManager.Core.Repositories
{
    public interface IEquipmentOrderRepository : IRepository<EquipmentOrder>
    {
        IEnumerable<EquipmentOrder> GetEquipmentOrdersWithUsersAndTypesAndOrderStatuses();
        EquipmentOrder GetSingleEquipmentOrderWithTypeAndOrderStatus(Expression<Func<EquipmentOrder, bool>> predicate);
    }
}
