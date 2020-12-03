using GymManager.Core.Domain;
using GymManager.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GymManager.Persistance.Repositories
{
    public class SupplementRepository : Repository<Supplement>, ISupplementRepository
    {
        public SupplementRepository(ApplicationDbContext context)
            :base(context)
        {
        }

        public IEnumerable<Supplement> GetSupplementsWithFlavorsAndTypes()
        {
            return ApplicationDbContext.Supplements
                .Include(s => s.Flavor)
                .Include(s => s.SupplementType)
                .ToList();
        }

        public Supplement GetSupplementWithFlavorAndType(Expression<Func<Supplement, bool>> predicate)
        {
            return ApplicationDbContext.Supplements
                .Include(s => s.Flavor)
                .Include(s => s.SupplementType)
                .SingleOrDefault(predicate);
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}