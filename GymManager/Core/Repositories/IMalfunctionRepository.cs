using GymManager.Core.Domain;
using System;
using System.Collections.Generic;

namespace GymManager.Core.Repositories
{
    public interface IMalfunctionRepository : IRepository<Malfunction>
    {
        IEnumerable<Malfunction> GetMalfunctionsWithEquipment();
    }
}
