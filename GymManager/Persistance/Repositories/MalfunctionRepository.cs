using GymManager.Core.Domain;
using GymManager.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System;

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

        public Malfunction GetMalfunctionWithEquipment(Expression<Func<Malfunction, bool>> predicate)
        {
            return ApplicationDbContext.Malfunctions
                .Include(m => m.Equipment)
                .SingleOrDefault(predicate);
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }


    }
}