using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using GymManager.Core;
using GymManager.Core.Repositories;
using GymManager.Models;
using GymManager.Persistance;
using GymManager.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace GymManager.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ApplicationDbContext>().InstancePerLifetimeScope();

            builder
              .RegisterType<UnitOfWork>()
              .As<IUnitOfWork>()
              .InstancePerRequest();


            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);

        }
    }
}