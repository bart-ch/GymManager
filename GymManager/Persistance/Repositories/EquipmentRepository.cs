using GymManager.Core.Domain;
using GymManager.Core.Repositories;
using GymManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;


namespace GymManager.Persistance.Repositories
{
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(ApplicationDbContext context)
            :base(context)
        {
        }

        public IEnumerable<Equipment> GetEquipmentWithAreasAndTypes()
        {
            return ApplicationDbContext.Equipment
                .Include(e => e.Area)
                .Include(e => e.Type)
                .ToList();
        }

        public Equipment GetSingleOrDefaultEquipmentWithAreaAndType(Expression<Func<Equipment, bool>> predicate)
        {
            return ApplicationDbContext.Equipment
                .Include(e => e.Area)
                .Include(e => e.Type)
                .SingleOrDefault(predicate);
        }

        public ApplicationDbContext ApplicationDbContext 
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}