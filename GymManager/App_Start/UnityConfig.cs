using GymManager.Core;
using GymManager.Core.Repositories;
using GymManager.Persistance;
using GymManager.Persistance.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace GymManager
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}