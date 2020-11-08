using GymManager.Core.Domain;
using GymManager.Core.Repositories;
using GymManager.Models;

namespace GymManager.Persistance.Repositories
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        public AreaRepository(ApplicationDbContext context)
            : base(context)
        {

        }
    }
}