using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Configuration;

using ThinkAnimal.Controllers;
using ThinkAnimal.Interface;
using ThinkAnimal.NodeDAL;


namespace ThinkAnimalApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();

            // Register the Controllers that should be injectable
            container.RegisterType<NodesController>();


            string connectionString = ConfigurationManager.ConnectionStrings["ThinkAnimal"].ConnectionString;

            // Register instances to be used when resolving constructor parameter dependencies
            container.RegisterInstance<INodeRepository>(NodeRepositoryFactory.Create(connectionString));


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
