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

        public ApplicationDbContext ApplicationDbContext 
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}