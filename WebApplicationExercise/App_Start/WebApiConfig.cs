﻿using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Unity;
using Unity.Lifetime;
using WebApplicationExercise.App_Start;
using WebApplicationExercise.Core;
using WebApplicationExercise.DataLayer.Interfaces;
using WebApplicationExercise.DataLayer.Repositories;
using WebApplicationExercise.Handlers;

namespace WebApplicationExercise
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IOrderRepository, OrderRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<CustomerManager, CustomerManager>(new HierarchicalLifetimeManager());
            container.RegisterType<MainDataContext, MainDataContext>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

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
