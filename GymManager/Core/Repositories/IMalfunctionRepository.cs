using GymManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymManager.Core.Repositories
{
    public interface IMalfunctionRepository : IRepository<Malfunction>
    {
        IEnumerable<Malfunction> GetMalfunctionsAndTheirRelatedEquipment();
    }
}