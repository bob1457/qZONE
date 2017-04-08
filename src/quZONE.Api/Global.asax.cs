using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.Practices.Unity;
using quZONE.Api.App_Start;
using quZONE.Api.Controllers;

namespace quZONE.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //// Create a new Unity dependency injection container
            //var unity = new UnityContainer();

            //// Register the Controllers that should be injectable
            //unity.RegisterType<TestController>();
            //unity.RegisterType<TestController>();

            // Register instances to be used when resolving constructor parameter dependencies
            //unity.RegisterInstance(new NameService("It Worked!!!"));

            // Finally, override the default dependency resolver with Unity
            //GlobalConfiguration.Configuration.DependencyResolver = new IoCContainer(unity);
        }
    }
}