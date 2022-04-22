using Autofac;
using FakeStore2.Persistence;
using FakeStore2.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static FakeStore2.Reads.Customer.GetAllCustomers;

namespace FakeStore2.CustomController
{
    //overwrites MVC controller factory with a controller that accepts parameters (our dbEntity in this case)
    public class CustomControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {

            /*****************************************************/
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler)
            // in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(Query).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.RegisterType<FakeStore2Entities>().As<IFakeStore2Entities>();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            var container = builder.Build();
            var mediator = container.Resolve<IMediator>();

            /*****************************************************/

            IController controller = Activator.CreateInstance(controllerType, new[] { mediator }) as Controller;
            return controller;
        }
    }
}