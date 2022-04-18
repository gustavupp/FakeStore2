using Autofac;
using FakeStore2.Persistence;
using FakeStore2.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FakeStore2.CustomController
{
    //overwrites MVC controller factory with a controller that accepts parameters (our dbEntity in this case)
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            //autofac setup 
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<FakeStore2Entities>().As<IFakeStore2Entities>();
            var Container = builder.Build();

            //isntead of manually instanciate FakeStire2Entities, let autofac do that for you.
            var context = Container.Resolve<IFakeStore2Entities>();

            IController controller = Activator.CreateInstance(controllerType, new[] { context }) as Controller;
            return controller;
        }
    }
}