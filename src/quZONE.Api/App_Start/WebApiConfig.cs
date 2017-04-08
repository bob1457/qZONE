using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;

using quZONE.Api.Infrastructure;
using quZONE.Data.Interfaces;
using quZONE.Data.Repositories;
using quZONE.Domain;
using quZONE.Domain.Services;
using quZONE.Services;
using quZONE.Services.TableManagement;
using quZONE.Services.UserProfile;
using quZONE.Services.WaitList;

using log4net;

namespace quZONE.Api.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //DI Configuration
            var container = new UnityContainer();
            container.RegisterType<ITestService, TestService>();   //to be re-implemented
            container.RegisterType<IUserProfileService, UserProfileService>();
            container.RegisterType<IWaitListService, WaitListService>();
            container.RegisterType<ITableManagementService, TableManagementService>();
            container.RegisterType<IWaitTimeService, WaitTimeService>();

            container.RegisterType<IUserProfileRepository, UserProfileRepository>();
            container.RegisterType<IWaitListRepository, WaitListRepository>();
            container.RegisterType<ITableManagementRepository, TableManagementRepository>();


            //container.RegisterType<IRepository<UserProfile>, Repository<UserProfile>>();



            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));

            config.DependencyResolver = new UnityDependencyResolver(container);

            // Web API configuration and services
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.XmlFormatter.AddUriPathExtensionMapping("xml", "application/xml");
            config.Formatters.JsonFormatter.AddUriPathExtensionMapping("json", "application/json");

            //log4net configuration
            log4net.Config.XmlConfigurator.Configure();

            // Web API routes - attribute routing
            config.MapHttpAttributeRoutes();

            //Service registration
            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());


            //Conventional routing
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithExtension",
                routeTemplate: "xml/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, ext = "xml" }
            );
        }
    }
}