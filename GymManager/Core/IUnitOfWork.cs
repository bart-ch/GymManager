﻿using GymManager.Core.Domain;
using GymManager.Core.Repositories;
using System;

namespace GymManager.Core
{
    public interface IUnitOfWork : IDisposable
    {
     
        IEquipmentRepository Equipment { get; }
        IRepository<Area> Areas { get; }
        IRepository<Domain.Type> Types { get; }
        ISupplementRepository Supplements { get; }
        IRepository<Flavor> Flavors { get; }
        IRepository<SupplementType> SupplementTypes { get; }
        IMalfunctionRepository Malfunctions { get; }

        int Complete();
    }
}
