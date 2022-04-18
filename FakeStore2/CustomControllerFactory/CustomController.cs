using FakeStore2.Persistence;
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
            var context = new FakeStore2Entities();
            IController controller = Activator.CreateInstance(controllerType, new[] { context }) as Controller;
            return controller;
        }
    }
}