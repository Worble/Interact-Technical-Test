using InteractTechinicalTestInfrastructure.EntityFramework;
using InteractTechinicalTestInfrastructure.Repositories.Litedb;
using InteractTechinicalTestInfrastructure.Repositories.Sqlite;
using InteractTechnicalTestDomain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace InteractTechnicalTest
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            //Sqlite Database
            var dbContextOptions = new DbContextOptionsBuilder<ProductContext>()
                .UseSqlite($"Data Source={ Helpers.DirectoryHelper.GetExecutingAssemblyFolder()}\\products_sqlite.db;");
            container.RegisterInstance(new ProductContext(dbContextOptions.Options));
            container.RegisterType<IProductRepository, SqliteProductRepository>(new InjectionConstructor(typeof(ProductContext)));

            //LiteDb Database
            //container.RegisterType<IProductRepository, LiteDbProductRepository>(new InjectionConstructor($"{ Helpers.DirectoryHelper.GetExecutingAssemblyFolder()}\\products_litedb.db"));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}