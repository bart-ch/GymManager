using GymManager.Core;
using GymManager.Core.Repositories;
using GymManager.Models;
using GymManager.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymManager.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IEquipmentRepository Equipment { get; }
        public IAreaRepository Areas { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Equipment = new EquipmentRepository(context);
            Areas = new AreaRepository(context);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}