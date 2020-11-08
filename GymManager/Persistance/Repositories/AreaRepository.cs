using GymManager.Core.Domain;
using GymManager.Core.Repositories;
using GymManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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