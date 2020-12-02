using GymManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GymManager.Core.Repositories
{
    public interface IMalfunctionRepository : IRepository<Malfunction>
    {
        IEnumerable<Malfunction> GetMalfunctionsWithEquipment();
        Malfunction GetMalfunctionWithEquipment(Expression<Func<Malfunction, bool>> predicate);
        
    }
}
