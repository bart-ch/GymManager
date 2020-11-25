﻿using GymManager.Core.Domain;
using GymManager.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

namespace GymManager.Persistance.Repositories
{
    public class EquipmentOrderRepository : Repository<EquipmentOrder>, IEquipmentOrderRepository
    {
        public EquipmentOrderRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public IEnumerable<EquipmentOrder> GetEquipmentOrdersWithTypesAndOrderStatuses()
        {
            return ApplicationDbContext.EquipmentOrders
                .Include(eo => eo.Type)
                .Include(eo => eo.OrderStatus)
                .ToList();
        }

        public EquipmentOrder GetSingleEquipmentOrderWithTypeAndOrderStatus(Expression<Func<EquipmentOrder, bool>> predicate)
        {
            return ApplicationDbContext.EquipmentOrders
                .Include(eo => eo.Type)
                .Include(eo => eo.OrderStatus)
                .SingleOrDefault(predicate);
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }


    }
}