using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1
{
    public class DependencyConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterType<MovieDbContext>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            
            // OPTIONAL: Register model binders that require DI.
            //builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            //builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            //builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            //builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            //builder.RegisterFilterProvider();

            // OPTIONAL: Enable action method parameter injection (RARE).
            // builder.InjectActionInvoker();

            // You can register controllers all at once using assembly scanning...
           // builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // ...or you can register individual controlllers manually.
           // builder.RegisterType<HomeController>().InstancePerRequest();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}