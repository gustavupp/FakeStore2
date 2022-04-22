using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using FakeStore2.CustomController;
using FakeStore2.Persistence;
using FakeStore2.Persistence.Interfaces;
using FakeStore2.Reads.Customer;
using MediatR;
using static FakeStore2.Reads.Customer.GetAllCustomers;

namespace FakeStore2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //register custom controller
            RegisterCustomControllerFactory();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //method to register custom controller
        private void RegisterCustomControllerFactory()
        {

            IControllerFactory factory = new CustomControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(factory);
        }
    }
}
