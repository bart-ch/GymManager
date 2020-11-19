using GymManager.Core.Domain;
using GymManager.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace GymManager.Persistance.Repositories
{
    public class MalfunctionRepository : Repository<Malfunction>, IMalfunctionRepository
    {
        public MalfunctionRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Malfunction> GetMalfunctionsWithEquipment()
        {
            return ApplicationDbContext.Malfunctions
                .Include(m => m.Equipment)
                .ToList();
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }


    }
}