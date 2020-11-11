using GymManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GymManager.Core.Repositories
{
    public interface ISupplementRepository
    {
        IEnumerable<Supplement> GetSupplementsWithFlavorsAndTypes();
        Supplement GetSingleOrDefaultSupplementWithFlavorAndType(Expression<Func<Supplement, bool>> predicate);
    }
}
